using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Store;

namespace Data.File;

internal class FileWheelCreator : FileRepository, IWheelCreator
{
    public FileWheelCreator(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<SaveResult<WheelSetting>> CreateWheelSetting(CreateWheelSetting wheelSetting)
    {
        throw new NotImplementedException();
    }
}