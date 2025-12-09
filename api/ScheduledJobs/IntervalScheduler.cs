using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Spinning;

namespace ScheduledJobs;

public class IntervalScheduler
{
    private readonly IDataAccess _dataAccess;
    private Timer? _timer;

    public IntervalScheduler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public void Start()
    {
        // Run immediately and then every hour
        _timer = new Timer(
            async _ => await CheckAndExecuteIntervals(),
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

            foreach (var interval in dueIntervals)
            {
                await ExecuteInterval(interval);
            }
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

            if (preset == null)
            {
                Console.WriteLine($"Preset '{interval.PresetName}' not found for interval '{interval.Name}'");
                return;
            }

            // Spin the wheel
            var spinResult = new WheelSpinningUseCase().SpinTheWheel(
                preset,
                new WheelSpinOptions { Mode = WheelSpinMode.Random }
            );

            var result = spinResult.Data?.GetLandedLabel() ?? "No result";
            Console.WriteLine($"Interval '{interval.Name}' executed: {result}");

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

    public delegate Task<string> IntervalExecutionHandler(string username, string presetName, string result);

    public event IntervalExecutionHandler? OnIntervalExecuted;

    private async Task ExecuteIntervalWithNotification(Interval interval)
    {
        try
        {
            // Get the preset
            var preset = await _dataAccess.WheelRetriever.GetWheelSetting(interval.PresetName);

            if (preset == null)
            {
                Console.WriteLine($"Preset '{interval.PresetName}' not found for interval '{interval.Name}'");
                return;
            }

            // Spin the wheel
            var spinResult = new WheelSpinningUseCase().SpinTheWheel(
                preset,
                new WheelSpinOptions { Mode = WheelSpinMode.Random }
            );

            var result = spinResult.Data?.GetLandedLabel() ?? "No result";
            Console.WriteLine($"Interval '{interval.Name}' executed: {result}");

            // Notify via event
            if (OnIntervalExecuted != null)
            {
                await OnIntervalExecuted(interval.Username, interval.PresetName, result);
            }

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
}
