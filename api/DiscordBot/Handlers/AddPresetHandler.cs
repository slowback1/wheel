using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("add")]
public class AddPresetHandler : BaseDiscordHandler, IDiscordHandler
{
    public AddPresetHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        var hasPipe = Context.Argument.Contains('|');
        if (!hasPipe)
            return
                "Preset name cannot be empty.  Please provide the argument in this format: <presetName>|<option1,option2,...>";

        var presetName = Context.Argument.Split('|')[0];
        var presetValues = Context.Argument.Split('|')[1].Split(',').ToArray();

        await DataAccess.WheelCreator.CreateWheelSetting(new CreateWheelSetting
        {
            Username = user.Username,
            Name = presetName,
            Slices = presetValues.Select(x => new WheelSlice
            {
                Label = x,
                Size = 1
            })
        });

        return $"Preset '{presetName}' added successfully.";
    }
}