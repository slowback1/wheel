using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.Wheel;

public class GetWheelSettingsUseCase : DataAccessorUseCase
{
    public GetWheelSettingsUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<IEnumerable<WheelSetting>>> GetWheelSettings()
    {
        return await Execute(GetSettings);
    }

    private async Task<FeatureResult<IEnumerable<WheelSetting>>> GetSettings()
    {
        var settings = await _dataAccess.WheelRetriever.GetWheelSettings();

        return FeatureResult<IEnumerable<WheelSetting>>.Ok(settings);
    }
}