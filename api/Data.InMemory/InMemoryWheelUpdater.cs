using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelUpdater : IWheelUpdater
{
    public Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting)
    {
        throw new NotImplementedException();
    }
}