using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Features;

public class WheelFeatures
{
    private readonly IWheelRetriever _wheelRetriever;

    public WheelFeatures(IWheelRetriever wheelRetriever)
    {
        _wheelRetriever = wheelRetriever;
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
}