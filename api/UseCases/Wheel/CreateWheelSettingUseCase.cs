using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Infrastructure.Messaging;
using UseCases.Shared;

namespace UseCases.Wheel;

public class CreateWheelSettingUseCase : DataAccessorUseCase
{
    public CreateWheelSettingUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<WheelSetting>> CreateWheelSetting(WheelSetting settingToCreate, string userToken)
    {
        return await Execute(() => Create(settingToCreate, userToken));
    }

    private async Task<FeatureResult<WheelSetting>> Create(WheelSetting settingToCreate, string userToken)
    {
        var username = TokenUtils.GetUserFromToken(userToken);

        var setting = new CreateWheelSetting
        {
            Name = settingToCreate.Name,
            Slices = settingToCreate.Slices,
            Username = username
        };

        var result = await _dataAccess.WheelCreator.CreateWheelSetting(setting);

        if (!result.SaveSuccessful) return FeatureResult<WheelSetting>.Error(new Exception(result.ErrorMessage));

        await MessageBus.PublishAsync(Messages.WheelSettingCreated, settingToCreate);

        return FeatureResult<WheelSetting>.Ok(settingToCreate);
    }
}