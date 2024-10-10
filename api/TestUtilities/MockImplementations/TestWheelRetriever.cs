using Common.Data;
using Common.Interfaces;
using TestUtilities.TestData;

namespace TestUtilities.MockImplementations;

public class TestWheelRetriever : IWheelRetriever
{
    public const string NotFoundId = "NotFound";
    public const string ErrorId = "Error";
    private bool ShouldThrowWhenGettingSettings { get; set; }

    public async Task<WheelSetting?> GetWheelSetting(string wheelId)
    {
        if (wheelId == NotFoundId) return null;
        if (wheelId == ErrorId) throw new Exception("Error");

        return TestWheelSettings.GetTestWheelSetting();
    }

    public async Task<IEnumerable<WheelSetting>> GetWheelSettings()
    {
        if (ShouldThrowWhenGettingSettings) throw new Exception("Error");
        return new List<WheelSetting>
        {
            TestWheelSettings.GetTestWheelSetting()
        };
    }

    public void SetShouldThrowWhenGettingAllSettings(bool shouldThrow)
    {
        ShouldThrowWhenGettingSettings = shouldThrow;
    }
}