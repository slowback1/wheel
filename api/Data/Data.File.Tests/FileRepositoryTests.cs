using Data.File;
using Data.File.Models;
using Data.File.Store;
using Data.Tests.File.Store;
using Data.Tests.TestUtilities;

namespace Data.Tests.File;

internal class TestFileRepository : FileRepository
{
    public TestFileRepository(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public IEnumerable<FileUser> GetUsers()
    {
        return Users;
    }

    public IEnumerable<FileWheel> GetWheels()
    {
        return Wheels;
    }

    public void SetUsers(IEnumerable<FileUser> users)
    {
        Users = users.ToList();
        SaveChanges();
    }

    public void SetWheels(IEnumerable<FileWheel> wheels)
    {
        Wheels = wheels.ToList();
        SaveChanges();
    }
}

public class FileRepositoryTests
{
    [Test]
    public void WhenConstructing_LoadsFileRepositoryIntoState()
    {
        var retriever = new TestFileStoreRetriever();
        retriever.UsersToGet = new[]
        {
            TestFileModels.GetTestFileUser()
        };
        retriever.WheelsToGet = new[]
        {
            TestFileModels.GetTestFileWheel()
        };

        var repository = new TestFileRepository(retriever);

        Assert.That(repository.GetUsers(), Is.EqualTo(retriever.UsersToGet));
        Assert.That(repository.GetWheels(), Is.EqualTo(retriever.WheelsToGet));
    }

    [Test]
    public void SaveChanges_SavesFileRepositoryState()
    {
        var retriever = new TestFileStoreRetriever();
        var repository = new TestFileRepository(retriever);
        var users = new[]
        {
            TestFileModels.GetTestFileUser()
        };
        var wheels = new[]
        {
            TestFileModels.GetTestFileWheel()
        };
        repository.SetUsers(users);
        repository.SetWheels(wheels);

        Assert.That(retriever.UsersToGet, Is.EqualTo(users));
        Assert.That(retriever.WheelsToGet, Is.EqualTo(wheels));
    }
}