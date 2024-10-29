using System;
using UseCases.Spinning.Spinners;

namespace UseCases.Spinning;

internal static class WheelSpinnerFactory
{
    public static IWheelSpinner Create(WheelSpinOptions options)
    {
        return options.Mode switch
        {
            WheelSpinMode.Random => new RandomWheelSpinner(),
            WheelSpinMode.Rigged => new RiggedWheelSpinner(options.RiggedSlice!.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(options.Mode), options.Mode, null)
        };
    }
}