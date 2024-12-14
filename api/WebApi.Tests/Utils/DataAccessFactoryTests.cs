using Data.File;
using Data.InMemory;
using WebApi.Config;
using WebApi.Utils;

namespace WebApi.Tests.Utils;

public class DataAccessFactoryTests
{
    [Test]
    public void UsesInMemoryDataAccessByDefault()
    {
        var options = new StorageConfig { StorageType = StorageType.InMemory };

        var dataAccess = DataAccessFactory.CreateDataAccess(options);
        Assert.IsInstanceOf<InMemoryDataAccess>(dataAccess);
    }

    [Test]
    public void UsesFileDataAccessWhenConfigured()
    {
        var options = new StorageConfig { StorageType = StorageType.File, FilePath = "test" };

        var dataAccess = DataAccessFactory.CreateDataAccess(options);
        Assert.IsInstanceOf<FileDataAccess>(dataAccess);
    }
}