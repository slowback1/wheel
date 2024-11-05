using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IWheelRetriever
{
    Task<WheelSetting?> GetWheelSetting(string wheelId);
    Task<IEnumerable<WheelSetting>> GetWheelSettings(string username);
}