using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using UseCases.Spinning.Spinners;

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
    private const int MaxRespinAttempts = 100;
    private static readonly ConcurrentDictionary<string, int> LastSpinResults = new();

    public FeatureResult<SpinResult> SpinTheWheel(WheelSetting wheel, WheelSpinOptions? options = null, string? userId = null)
    {
        if (!WheelHasSlices(wheel))
            return Error("Error: Wheel has no slices.  Please add at least one slice to the wheel before spinning.");

        var normalizedOptions = NormalizeOptions(options);

        var spinner = WheelSpinnerFactory.Create(normalizedOptions);

        var result = spinner.Spin(wheel);
        
        // Prevent duplicate spins for the same user and store the result
        if (!string.IsNullOrEmpty(userId))
        {
            result = PreventDuplicateSpin(wheel, spinner, result, userId);
            LastSpinResults[userId] = result.SliceLanded;
        }

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

    private WheelSpinOptions NormalizeOptions(WheelSpinOptions? options)
    {
        var normalizedOptions = options ?? new WheelSpinOptions();

        if (normalizedOptions.Mode == null)
            normalizedOptions.Mode = WheelSpinMode.Random;


        return normalizedOptions;
    }

    private SpinResult PreventDuplicateSpin(WheelSetting wheel, IWheelSpinner spinner, SpinResult result, string userId)
    {
        // Check if wheel has only one slice - allow duplicates
        if (wheel.Slices.Count() == 1)
        {
            return result;
        }

        // Check if this user has spun before
        if (!LastSpinResults.TryGetValue(userId, out var lastResult))
        {
            return result;
        }

        var attempts = 0;

        // Keep spinning until we get a different result
        while (result.SliceLanded == lastResult && attempts < MaxRespinAttempts)
        {
            result = spinner.Spin(wheel);
            attempts++;
        }

        return result;
    }
}