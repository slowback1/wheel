using Common.Data;

namespace TestUtilities.TestData;

public static class TestWheelSettings
{
    public static WheelSetting GetTestWheelSetting()
    {
        return new WheelSetting
        {
            Name = "Name",
            Slices = new[] { GetTestWheelSlice() }
        };
    }

    public static WheelSlice GetTestWheelSlice()
    {
        return new WheelSlice
        {
            Label = "Label",
            Size = 1
        };
    }
}