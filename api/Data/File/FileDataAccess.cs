using Common.Interfaces;

namespace Data.File;

public class FileDataAccess : IDataAccess
{
    public IWheelCreator WheelCreator { get; }
    public IWheelRetriever WheelRetriever { get; }
    public IUserCreator UserCreator { get; }
    public IUserRetriever UserRetriever { get; }
}