using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IWheelDeleter
{
    Task<SaveResult<WheelSetting>> DeleteWheelSetting(string wheelId);
}