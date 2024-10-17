using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Features;

public class WheelFeatures
{
    private readonly IWheelCreator _wheelCreator;
    private readonly IWheelRetriever _wheelRetriever;

    public WheelFeatures(IWheelRetriever wheelRetriever, IWheelCreator wheelCreator)
    {
        _wheelRetriever = wheelRetriever;
        _wheelCreator = wheelCreator;
    }

    public async Task<FeatureResult<WheelSetting>> GetWheelSetting(string wheelId)
    {
        try
        {
            var wheelSetting = await _wheelRetriever.GetWheelSetting(wheelId);

            if (wheelSetting is null) return FeatureResult<WheelSetting>.NotFound();

            return FeatureResult<WheelSetting>.Ok(wheelSetting);
        }
        catch (Exception e)
        {
            return FeatureResult<WheelSetting>.Error(e);
        }
    }

    public async Task<FeatureResult<IEnumerable<WheelSetting>>> GetWheelSettings()
    {
        try
        {
            var settings = await _wheelRetriever.GetWheelSettings();

            return FeatureResult<IEnumerable<WheelSetting>>.Ok(settings);
        }
        catch (Exception e)
        {
            return FeatureResult<IEnumerable<WheelSetting>>.Error(e);
        }
    }

    public async Task<FeatureResult<WheelSetting>> CreateWheelSetting(WheelSetting settingToCreate)
    {
        try
        {
            var result = await _wheelCreator.CreateWheelSetting(settingToCreate);

            if (!result.SaveSuccessful) return FeatureResult<WheelSetting>.Error(new Exception(result.ErrorMessage));

            return FeatureResult<WheelSetting>.Ok(settingToCreate);
        }
        catch (Exception e)
        {
            return FeatureResult<WheelSetting>.Error(e);
        }
    }
}