using Data.File;
using Data.Tests.File.Store;
using Data.Tests.TestUtilities;
using TestUtilities.TestData;

namespace Data.Tests.File;

public class FileUserCreatorTests
{
    [Test]
    public async Task CreateUser_WhenCalled_IndicatesSaveSuccessful()
    {
        var user = TestUsers.GetTestCreateUser();
        var store = new TestFileStoreRetriever();
        var creator = new FileUserCreator(store);

        var result = await creator.CreateUser(user);

        Assert.That(result.SaveSuccessful, Is.True);
    }

    [Test]
    public async Task CreateUser_WhenCalled_SavesUserToStore()
    {
        var user = TestUsers.GetTestCreateUser();
        var store = new TestFileStoreRetriever();
        var creator = new FileUserCreator(store);

        var result = await creator.CreateUser(user);

        Assert.That(store.UsersToGet.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task CreateUser_WhenCalledWithDuplicateUsername_IndicatesSaveFailed()
    {
        var user = TestUsers.GetTestCreateUser();
        var store = new TestFileStoreRetriever();
        store.UsersToGet = new[]
        {
            TestFileModels.GetTestFileUser()
        };

        var existingUser = store.UsersToGet.First();

        user.Username = existingUser.Username;

        var creator = new FileUserCreator(store);

        var result = await creator.CreateUser(user);

        Assert.That(result.SaveSuccessful, Is.False);
        Assert.That(result.ErrorMessage, Is.EqualTo("User already exists"));
    }

    [Test]
    public async Task CreateUser_WhenCalled_ReturnsSavedEntity()
    {
        var user = TestUsers.GetTestCreateUser();
        var store = new TestFileStoreRetriever();
        var creator = new FileUserCreator(store);

        var result = await creator.CreateUser(user);

        Assert.That(result.SavedEntity, Is.Not.Null);
        Assert.That(result.SavedEntity.Username, Is.EqualTo(user.Username));
    }
}