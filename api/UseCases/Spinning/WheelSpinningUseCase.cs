using System;
using System.Linq;
using Common.Data;

namespace UseCases.Spinning;

public struct WheelSpinOptions
{
    public int? RiggedSlice { get; set; }
    public WheelSpinMode Mode { get; set; }
}

public enum WheelSpinMode
{
    Random,
    Rigged,
    Distribution
}

public class WheelSpinningUseCase
{
    public FeatureResult<SpinResult> SpinTheWheel(WheelSetting wheel, WheelSpinOptions? options = null)
    {
        if (!WheelHasSlices(wheel))
            return Error("Error: Wheel has no slices.  Please add at least one slice to the wheel before spinning.");

        var normalizedOptions = NormalizeOptions(options);

        var spinner = WheelSpinnerFactory.Create(normalizedOptions);

        return FeatureResult<SpinResult>.Ok(spinner.Spin(wheel));
    }

    private bool WheelHasSlices(WheelSetting wheel)
    {
        return wheel.Slices != null && wheel.Slices.Any();
    }

    private FeatureResult<SpinResult> Error(string message)
    {
        return FeatureResult<SpinResult>.Error(new Exception(message));
    }

    private SpinResult RiggedResult(int riggedSlice, WheelSetting wheel)
    {
        return new SpinResult
        {
            WheelUsed = wheel,
            SliceLanded = riggedSlice
        };
    }

    private WheelSpinOptions NormalizeOptions(WheelSpinOptions? options)
    {
        var normalizedOptions = options ?? new WheelSpinOptions();

        if (normalizedOptions.Mode == null)
            normalizedOptions.Mode = WheelSpinMode.Random;


        return normalizedOptions;
    }
}