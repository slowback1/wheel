using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("list-intervals")]
public class ListIntervalsHandler : BaseDiscordHandler, IDiscordHandler
{
    public ListIntervalsHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        var intervals = (await DataAccess.IntervalRetriever.GetIntervalsForUser(user.Username)).ToArray();

        if (!intervals.Any())
            return "No intervals found.";

        var intervalList = string.Join("\n", intervals.Select(i => 
            $"- {i.Name} (preset: {i.PresetName}, frequency: {i.Frequency})"));

        return $"Your intervals:\n{intervalList}";
    }
}
