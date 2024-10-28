using System;
using System.Linq;
using Common.Data;

namespace UseCases.Spinning;

public struct WheelSpinOptions
{
    public int? RiggedSlice { get; set; }
}

public class WheelSpinningUseCase
{
    public FeatureResult<SpinResult> SpinTheWheel(WheelSetting wheel, WheelSpinOptions? options = null)
    {
        if (!WheelHasSlices(wheel))
            return Error("Error: Wheel has no slices.  Please add at least one slice to the wheel before spinning.");

        var normalizedOptions = NormalizeOptions(options);

        if (normalizedOptions.RiggedSlice.HasValue)
            return FeatureResult<SpinResult>.Ok(RiggedResult(normalizedOptions.RiggedSlice.Value, wheel));

        var result = new SpinResult();
        result.WheelUsed = wheel;
        result.SliceLanded = new Random().Next(0, wheel.Slices.Count());
        return FeatureResult<SpinResult>.Ok(result);
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
        return options ?? new WheelSpinOptions();
    }
}