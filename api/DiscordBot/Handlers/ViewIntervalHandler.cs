using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("view-interval")]
public class ViewIntervalHandler : BaseDiscordHandler, IDiscordHandler
{
    public ViewIntervalHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        if (string.IsNullOrWhiteSpace(Context.Argument))
            return "Please provide an interval name.";

        var intervalName = Context.Argument.Trim();
        var interval = await DataAccess.IntervalRetriever.GetInterval(user.Username, intervalName);

        if (interval == null)
            return $"Interval '{intervalName}' not found.";

        var lastRun = interval.LastRunTime == DateTime.MinValue 
            ? "Never" 
            : interval.LastRunTime.ToString("yyyy-MM-dd HH:mm:ss UTC");

        var nextRun = interval.NextRunTime.ToString("yyyy-MM-dd HH:mm:ss UTC");

        return $"Interval: {interval.Name}\n" +
               $"Preset: {interval.PresetName}\n" +
               $"Frequency: {interval.Frequency}\n" +
               $"Channel ID: {interval.ChannelId}\n" +
               $"Last Run: {lastRun}\n" +
               $"Next Run: {nextRun}";
    }
}
