using System;
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

    public async Task<FeatureResult<IEnumerable<WheelSetting>>> GetWheelSettings(string userToken)
    {
        return await Execute(() => GetSettings(userToken));
    }

    private async Task<FeatureResult<IEnumerable<WheelSetting>>> GetSettings(string userToken)
    {
        var username = TokenUtils.GetUserFromToken(userToken);

        if (username is null) return FeatureResult<IEnumerable<WheelSetting>>.Error(new Exception("Invalid token"));

        var settings = await _dataAccess.WheelRetriever.GetWheelSettings(username);

        return FeatureResult<IEnumerable<WheelSetting>>.Ok(settings);
    }
}