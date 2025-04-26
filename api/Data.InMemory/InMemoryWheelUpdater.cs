using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelUpdater : IWheelUpdater
{
    public async Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting)
    {
        var stored = GetWheelSetting(wheelSetting.Name);

        if (stored is null) return SaveResult<WheelSetting>.Failure("Wheel does not exist");

        stored.Name = wheelSetting.Name;
        stored.Slices = wheelSetting.Slices;

        return SaveResult<WheelSetting>.Success(stored);
    }

    private WheelSetting? GetWheelSetting(string name)
    {
        var wheel = InMemoryStore.Wheels.SelectMany(x => x.Value)
            .FirstOrDefault(w => w.Name == name);

        return wheel;
    }
}