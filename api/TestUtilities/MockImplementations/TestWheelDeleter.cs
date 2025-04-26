using Common.Data;
using Common.Interfaces;
using TestUtilities.TestData;

namespace TestUtilities.MockImplementations;

public class TestWheelDeleter : IWheelDeleter
{
    public string LastDeletedId { get; private set; } = string.Empty;

    public async Task<SaveResult<WheelSetting>> DeleteWheelSetting(string wheelId)
    {
        LastDeletedId = wheelId;

        var wheel = TestWheelSettings.GetTestWheelSetting();
        wheel.Name = wheelId;

        return SaveResult<WheelSetting>.Success(wheel);
    }
}