using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelDeleter : IWheelDeleter
{
    public Task<SaveResult<WheelSetting>> DeleteWheelSetting(string wheelId)
    {
        var storedKey = InMemoryStore.Wheels.FirstOrDefault(x => x.Value.Any(y => y.Name == wheelId));

        if (storedKey.Value == null) return Task.FromResult(SaveResult<WheelSetting>.Failure("Wheel not found"));

        storedKey.Value.RemoveAll(x => x.Name == wheelId);
        InMemoryStore.Wheels[storedKey.Key] = storedKey.Value;

        return Task.FromResult(SaveResult<WheelSetting>.Success(new WheelSetting
        {
            Name = wheelId
        }));
    }
}