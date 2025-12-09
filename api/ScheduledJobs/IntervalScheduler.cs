using Common.Data;
using Common.Interfaces;
using UseCases.Spinning;

namespace ScheduledJobs;

public class IntervalScheduler
{
    private readonly IDataAccess _dataAccess;
    private readonly Func<ulong, string, Task> _sendMessageToChannel;
    private Timer? _timer;

    public IntervalScheduler(IDataAccess dataAccess, Func<ulong, string, Task> sendMessageToChannel)
    {
        _dataAccess = dataAccess;
        _sendMessageToChannel = sendMessageToChannel;
    }

    public void Start()
    {
        // Run immediately and then every hour
        _timer = new Timer(
            _ =>
            {
                // Use Task.Run to avoid blocking and handle exceptions
                Task.Run(async () =>
                {
                    try
                    {
                        await CheckAndExecuteIntervals();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in interval scheduler: {ex.Message}");
                    }
                });
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromHours(1)
        );
    }

    public void Stop()
    {
        _timer?.Dispose();
        _timer = null;
    }

    private async Task CheckAndExecuteIntervals()
    {
        try
        {
            var dueIntervals = await _dataAccess.IntervalRetriever.GetIntervalsDueForExecution();

            foreach (var interval in dueIntervals) await ExecuteInterval(interval);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking intervals: {ex.Message}");
        }
    }

    private async Task ExecuteInterval(Interval interval)
    {
        try
        {
            // Get the preset
            var preset = await _dataAccess.WheelRetriever.GetWheelSetting(interval.PresetName);
            string message;
            if (preset == null)
            {
                message = $"Preset '{interval.PresetName}' not found for interval '{interval.Name}'";
            }
            else
            {
                // Spin the wheel
                var spinResult = new WheelSpinningUseCase().SpinTheWheel(
                    preset,
                    new WheelSpinOptions { Mode = WheelSpinMode.Random }
                );

                var result = spinResult.Data?.GetLandedLabel() ?? "No result";
                message = $"Interval '{interval.Name}' executed: {result}";
            }

            Console.WriteLine($"Sending interval message to channel {interval.ChannelId}: {message}");
            await _sendMessageToChannel(interval.ChannelId, message);

            // Update the last run time and calculate next run time
            var now = DateTime.UtcNow;
            var nextRun = CalculateNextRunTime(now, interval.Frequency);

            await _dataAccess.IntervalUpdater.UpdateIntervalLastRun(
                interval.Username,
                interval.Name,
                now,
                nextRun
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing interval '{interval.Name}': {ex.Message}");
        }
    }

    private static DateTime CalculateNextRunTime(DateTime from, IntervalFrequency frequency)
    {
        return frequency switch
        {
            IntervalFrequency.Hourly => from.AddHours(1),
            IntervalFrequency.Daily => from.AddDays(1),
            IntervalFrequency.Weekly => from.AddDays(7),
            IntervalFrequency.Monthly => from.AddMonths(1),
            IntervalFrequency.Quarterly => from.AddMonths(3),
            IntervalFrequency.Yearly => from.AddYears(1),
            _ => from.AddHours(1)
        };
    }
}