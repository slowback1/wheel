using Common.Data;
using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestWheelUpdater : IWheelUpdater
{
    public WheelSetting LastUpdated { get; private set; }

    public async Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting)
    {
        LastUpdated = wheelSetting;

        return SaveResult<WheelSetting>.Success(wheelSetting);
    }
}