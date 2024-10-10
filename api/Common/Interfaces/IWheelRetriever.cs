using System.Collections.Generic;
using Common.Data;

namespace Common.Interfaces;

public interface IWheelRetriever
{
    WheelSetting GetWheelSetting(string wheelId);
    IEnumerable<WheelSetting> GetWheelSettings();
}