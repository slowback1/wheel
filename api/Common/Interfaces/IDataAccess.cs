namespace Common.Interfaces;

public interface IDataAccess
{
    IWheelCreator WheelCreator { get; }
    IWheelRetriever WheelRetriever { get; }
}