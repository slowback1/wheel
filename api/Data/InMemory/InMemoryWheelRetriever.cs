using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryWheelRetriever : IWheelRetriever
{
    public async Task<WheelSetting?> GetWheelSetting(string wheelId)
    {
        return InMemoryStore.Wheels.FirstOrDefault(wheel => wheel.Name == wheelId);
    }

    public async Task<IEnumerable<WheelSetting>> GetWheelSettings()
    {
        return InMemoryStore.Wheels;
    }
}