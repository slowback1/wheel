using Common.Data;

namespace UseCases.Spinning.Spinners;

internal interface IWheelSpinner
{
    SpinResult Spin(WheelSetting wheel);
}