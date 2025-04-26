using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IWheelUpdater
{
    Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting);
}