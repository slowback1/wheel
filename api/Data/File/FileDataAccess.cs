using Common.Interfaces;
using Data.File.Store;

namespace Data.File;

public class FileDataAccess : IDataAccess
{
    public FileDataAccess(FileStorageSettings settings)
    {
        var store = new FileStoreRetriever(settings);

        WheelCreator = new FileWheelCreator(store);
    }

    public IWheelCreator WheelCreator { get; }
    public IWheelRetriever WheelRetriever { get; }
    public IUserCreator UserCreator { get; }
    public IUserRetriever UserRetriever { get; }
}