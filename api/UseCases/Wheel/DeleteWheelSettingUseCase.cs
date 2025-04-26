using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.Wheel;

public class DeleteWheelSettingUseCase : DataAccessorUseCase
{
    public DeleteWheelSettingUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<bool>> DeleteWheelSetting(string wheelId)
    {
        var result = await _dataAccess.WheelDeleter.DeleteWheelSetting(wheelId);

        return FeatureResult<bool>.Ok(true);
    }
}