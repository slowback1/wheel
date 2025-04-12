using Data.InMemory;

namespace Data.Tests.InMemory;

public class InMemoryDataAccessTests
{
    [Test]
    [TestCase(nameof(InMemoryDataAccess.WheelCreator), typeof(InMemoryWheelCreator))]
    [TestCase(nameof(InMemoryDataAccess.WheelRetriever), typeof(InMemoryWheelRetriever))]
    [TestCase(nameof(InMemoryDataAccess.UserCreator), typeof(InMemoryUserCreator))]
    [TestCase(nameof(InMemoryDataAccess.UserRetriever), typeof(InMemoryUserRetriever))]
    public void Properties_ReturnCorrectTypes(string propertyName, Type expectedType)
    {
        var dataAccess = new InMemoryDataAccess();
        var property = typeof(InMemoryDataAccess).GetProperty(propertyName);
        var value = property.GetValue(dataAccess);
        Assert.That(value, Is.InstanceOf(expectedType));
    }
}