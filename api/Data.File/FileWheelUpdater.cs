using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Data.File;

public class FileWheelUpdater : IWheelUpdater
{
    public Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting)
    {
        throw new NotImplementedException();
    }
}