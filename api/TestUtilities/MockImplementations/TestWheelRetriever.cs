using Common.Data;
using Common.Interfaces;
using TestUtilities.TestData;

namespace TestUtilities.MockImplementations;

public class TestWheelRetriever : IWheelRetriever
{
    public const string NotFoundId = "NotFound";
    public const string ErrorId = "Error";

    public IEnumerable<WheelSetting> WheelSettingsToReturn { get; set; } =
        new[] { TestWheelSettings.GetTestWheelSetting() };

    private bool ShouldThrowWhenGettingSettings { get; set; }

    public async Task<WheelSetting?> GetWheelSetting(string wheelId)
    {
        if (wheelId == NotFoundId) return null;
        if (wheelId == ErrorId) throw new Exception("Error");

        return TestWheelSettings.GetTestWheelSetting();
    }

    public async Task<IEnumerable<WheelSetting>> GetWheelSettings(string username)
    {
        if (ShouldThrowWhenGettingSettings) throw new Exception("Error");
        return WheelSettingsToReturn;
    }

    public void SetShouldThrowWhenGettingAllSettings(bool shouldThrow)
    {
        ShouldThrowWhenGettingSettings = shouldThrow;
    }
}