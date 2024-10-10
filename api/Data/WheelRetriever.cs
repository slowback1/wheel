using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Newtonsoft.Json;

namespace Data;

public class WheelRetriever : IWheelRetriever
{
    public async Task<WheelSetting?> GetWheelSetting(string wheelId)
    {
        var wheelSettings = await GetWheelSettingsFromFile();

        return wheelSettings?.FirstOrDefault(w => w.Name == wheelId);
    }

    public async Task<IEnumerable<WheelSetting>> GetWheelSettings()
    {
        return await GetWheelSettingsFromFile() ?? new List<WheelSetting>();
    }

    private async Task<IEnumerable<WheelSetting>?> GetWheelSettingsFromFile()
    {
        var filePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\WheelData.json");

        var stringData = await File.ReadAllTextAsync(filePath);

        return JsonConvert.DeserializeObject<IEnumerable<WheelSetting>>(stringData);
    }
}