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

    public async Task<FeatureResult<WheelSetting>> CreateWheelSetting(WheelSetting settingToCreate)
    {
        return await Execute(() => Create(settingToCreate));
    }

    private async Task<FeatureResult<WheelSetting>> Create(WheelSetting settingToCreate)
    {
        var result = await _dataAccess.WheelCreator.CreateWheelSetting(settingToCreate);

        if (!result.SaveSuccessful) return FeatureResult<WheelSetting>.Error(new Exception(result.ErrorMessage));

        await MessageBus.PublishAsync(Messages.WheelSettingCreated, settingToCreate);

        return FeatureResult<WheelSetting>.Ok(settingToCreate);
    }
}