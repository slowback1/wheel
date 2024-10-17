using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestDataAccess : IDataAccess
{
    public IWheelCreator WheelCreator { get; } = new TestWheelCreator();
    public IWheelRetriever WheelRetriever { get; } = new TestWheelRetriever();
}