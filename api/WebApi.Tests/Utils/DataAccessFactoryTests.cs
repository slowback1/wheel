using Data.InMemory;
using WebApi.Utils;

namespace WebApi.Tests.Utils;

public class DataAccessFactoryTests
{
    [Test]
    public void UsesInMemoryDataAccessByDefault()
    {
        var dataAccess = DataAccessFactory.CreateDataAccess();
        Assert.IsInstanceOf<InMemoryDataAccess>(dataAccess);
    }
}