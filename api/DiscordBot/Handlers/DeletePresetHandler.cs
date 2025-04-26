using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;
using UseCases.Wheel;

namespace DiscordBot.Handlers;

[DiscordAction("delete")]
public class DeletePresetHandler : BaseDiscordHandler, IDiscordHandler
{
    public DeletePresetHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var useCase = new DeleteWheelSettingUseCase(DataAccess);

        var wheelId = Context.Argument;

        var result = await useCase.DeleteWheelSetting(wheelId);

        if (result.Status == FeatureResultStatus.Ok)
            return $"Preset {wheelId} deleted successfully.";
        if (result.Status == FeatureResultStatus.NotFound)
            return $"Preset {wheelId} not found.";
        return $"Failed to delete preset {wheelId}.";
    }
}