using Common.Interfaces;
using DiscordBot.Models;
using UseCases.Spinning;

namespace DiscordBot.Handlers;

[DiscordAction("spin-preset")]
public class SpinPresetHandler : BaseDiscordHandler, IDiscordHandler
{
    public SpinPresetHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        var preset = await DataAccess.WheelRetriever.GetWheelSetting(Context.Argument);

        if (preset == null) return $"Preset '{Context.Argument}' not found.";

        var spinResult =
            new WheelSpinningUseCase().SpinTheWheel(preset, new WheelSpinOptions { Mode = WheelSpinMode.Random });

        return spinResult.Data!.GetLandedLabel();
    }
}