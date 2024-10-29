using Common.Data;

namespace UseCases.Spinning.Spinners;

internal class RiggedWheelSpinner : IWheelSpinner
{
    private readonly int _riggedSlice;

    public RiggedWheelSpinner(int riggedSlice)
    {
        _riggedSlice = riggedSlice;
    }

    public SpinResult Spin(WheelSetting wheel)
    {
        return new SpinResult
        {
            WheelUsed = wheel,
            SliceLanded = _riggedSlice
        };
    }
}