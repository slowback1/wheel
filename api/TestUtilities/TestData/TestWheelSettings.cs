using Common.Data;

namespace TestUtilities.TestData;

public static class TestWheelSettings
{
    public static CreateWheelSetting GetTestWheelSetting()
    {
        return new CreateWheelSetting
        {
            Name = "Name",
            Slices = new[] { GetTestWheelSlice() },
            Username = "test"
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