using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.Wheel;

public class GetWheelSettingUseCase : DataAccessorUseCase
{
    public GetWheelSettingUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<WheelSetting>> GetWheelSetting(string wheelId)
    {
        return await Execute(() => GetSetting(wheelId));
    }

    private async Task<FeatureResult<WheelSetting>> GetSetting(string wheelId)
    {
        var wheelSetting = await _dataAccess.WheelRetriever.GetWheelSetting(wheelId);

        if (wheelSetting is null) return FeatureResult<WheelSetting>.NotFound();

        return FeatureResult<WheelSetting>.Ok(wheelSetting);
    }
}