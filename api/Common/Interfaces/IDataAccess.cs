namespace Common.Interfaces;

public interface IDataAccess
{
    IWheelCreator WheelCreator { get; }
    IWheelRetriever WheelRetriever { get; }
    IUserCreator UserCreator { get; }
    IUserRetriever UserRetriever { get; }
}