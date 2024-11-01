using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelCreator : IWheelCreator
{
    public async Task<SaveResult<WheelSetting>> CreateWheelSetting(WheelSetting wheelSetting)
    {
        if (SettingExists(wheelSetting.Name))
            return SaveResult<WheelSetting>.Failure(
                $"A wheel with that name '{wheelSetting.Name}' already exists.");

        InMemoryStore.Wheels.Add(wheelSetting);

        return await Task.FromResult(SaveResult<WheelSetting>.Success(wheelSetting));
    }

    private bool SettingExists(string name)
    {
        return InMemoryStore.Wheels.Any(w => w.Name == name);
    }
}