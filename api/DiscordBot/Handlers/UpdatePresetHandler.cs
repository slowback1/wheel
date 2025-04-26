using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;
using UseCases.Wheel;

namespace DiscordBot.Handlers;

[DiscordAction("update")]
public class UpdatePresetHandler : BaseDiscordHandler, IDiscordHandler
{
    public UpdatePresetHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
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

        var wheelSetting = new WheelSetting
        {
            Name = presetName,
            Slices = presetValues.Select(x => new WheelSlice
            {
                Label = x,
                Size = 1
            })
        };

        var useCase = new UpdateWheelUseCase(DataAccess);
        var result = await useCase.UpdateWheelSetting(presetName, wheelSetting);

        if (result.Status == FeatureResultStatus.Ok)
            return $"Preset '{presetName}' updated successfully.";
        return $"Error updating preset: {result.Exception?.Message ?? "Unknown error"}";
    }
}