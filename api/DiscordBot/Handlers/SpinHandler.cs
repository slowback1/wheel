using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;
using DiscordBot.Utils;
using UseCases.Spinning;

namespace DiscordBot.Handlers;

[DiscordAction("spin")]
public class SpinHandler : BaseDiscordHandler, IDiscordHandler
{
    public SpinHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var spinUseCase = new WheelSpinningUseCase();

        var wheel = DiscordMessageParser.ParseWheelSetting(Context.Command);

        var spinResult = spinUseCase.SpinTheWheel(wheel, new WheelSpinOptions { Mode = WheelSpinMode.Random });

        if (spinResult.Status != FeatureResultStatus.Ok) return "something went wrong";

        return spinResult.Data!.GetLandedLabel();
    }
}