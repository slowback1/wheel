using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.Wheel;

public class UpdateWheelUseCase : DataAccessorUseCase
{
    public UpdateWheelUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<WheelSetting>> UpdateWheelSetting(string wheelId, WheelSetting wheelSetting)
    {
        return null;
    }
}