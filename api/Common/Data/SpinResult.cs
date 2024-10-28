using System.Linq;

namespace Common.Data;

public class SpinResult
{
    public WheelSetting WheelUsed { get; set; }
    public int SliceLanded { get; set; }

    public string GetLandedLabel()
    {
        return WheelUsed.Slices.ElementAt(SliceLanded).Label;
    }
}