using Common.Interfaces;
using Data.File;
using Data.File.Store;
using Data.InMemory;
using WebApi.Config;

namespace WebApi.Utils;

public static class DataAccessFactory
{
    public static IDataAccess CreateDataAccess(StorageConfig config)
    {
        return config.StorageType switch
        {
            StorageType.InMemory => new InMemoryDataAccess(),
            StorageType.File => new FileDataAccess(new FileStorageSettings(config.FilePath ?? "")),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}