using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelCreator : IWheelCreator
{
    public async Task<SaveResult<WheelSetting>> CreateWheelSetting(CreateWheelSetting wheelSetting)
    {
        if (SettingExists(wheelSetting.Name))
            return SaveResult<WheelSetting>.Failure(
                $"A wheel with that name '{wheelSetting.Name}' already exists.");

        if (!InMemoryStore.Wheels.ContainsKey(wheelSetting.Username))
            InMemoryStore.Wheels.Add(wheelSetting.Username, new List<WheelSetting>());

        InMemoryStore.Wheels[wheelSetting.Username].Add(wheelSetting);

        return await Task.FromResult(SaveResult<WheelSetting>.Success(wheelSetting));
    }

    private bool SettingExists(string name)
    {
        return InMemoryStore.Wheels
            .SelectMany(w => w.Value)
            .Any(w => w.Name == name);
    }
}