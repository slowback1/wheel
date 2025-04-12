using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("list-presets")]
public class ListPresetsHandler : BaseDiscordHandler, IDiscordHandler
{
    public ListPresetsHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        var wheelSettings = (await DataAccess.WheelRetriever.GetWheelSettings(user.Username)).ToArray();

        if (!wheelSettings.Any())
            return "No presets found.";

        var presetList = string.Join(", ", wheelSettings.Select(w => w.Name));

        return $"Your presets: {presetList}";
    }
}