using System;
using System.Linq;
using Common.Data;

namespace UseCases.Spinning.Spinners;

internal class RandomWheelSpinner : IWheelSpinner
{
    public SpinResult Spin(WheelSetting wheel)
    {
        var result = new SpinResult();
        result.WheelUsed = wheel;
        result.SliceLanded = new Random().Next(0, wheel.Slices.Count());
        return result;
    }
}