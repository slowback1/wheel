using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;
using UseCases.Wheel;

namespace DiscordBot.Handlers;

[DiscordAction("describe")]
public class DescribePresetHandler : BaseDiscordHandler, IDiscordHandler
{
    public DescribePresetHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        var presetName = Context.Argument.Trim();
        if (string.IsNullOrEmpty(presetName))
            return "Preset name cannot be empty. Please provide a preset name.";

        var useCase = new GetWheelSettingUseCase(DataAccess);
        var getResult = await useCase.GetWheelSetting(presetName);

        if (getResult.Status == FeatureResultStatus.Ok)
        {
            var slices = string.Join(',', getResult.Data!.Slices.Select(x => x.Label));
            return slices;
        }

        return $"Error retrieving preset: {getResult.Exception?.Message ?? "Unknown error"}";
    }
}