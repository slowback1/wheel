using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IWheelCreator
{
    Task<SaveResult<WheelSetting>> CreateWheelSetting(CreateWheelSetting wheelSetting);
}