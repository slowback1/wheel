using Common.Data;
using Data.InMemory;

namespace Data.Tests.InMemory;

public class InMemoryUserCreatorTests
{
    [TearDown]
    public void TearDown()
    {
        InMemoryStore.Users.Clear();
    }

    [Test]
    public async Task CreateUser_WhenCalled_ReturnsSaveResultWithUser()
    {
        var user = new CreateUser
        {
            Username = "test",
            Password = "password"
        };
        var creator = new InMemoryUserCreator();

        var result = await creator.CreateUser(user);

        Assert.IsTrue(result.SaveSuccessful);
        Assert.That(result.SavedEntity.Username, Is.EqualTo(user.Username));
    }

    [Test]
    public async Task CreateUser_WhenCalled_SavesUserToTheStore()
    {
        var user = new CreateUser
        {
            Username = "test",
            Password = "password"
        };
        var creator = new InMemoryUserCreator();

        await creator.CreateUser(user);

        var storedUsers = InMemoryStore.Users;

        Assert.That(storedUsers, Has.Count.EqualTo(1));
        Assert.That(storedUsers[0].Username, Is.EqualTo(user.Username));
    }

    [Test]
    public async Task CreateUser_WhenCalledWithDuplicateUsername_ReturnsSaveResultWithFailure()
    {
        var user = new CreateUser
        {
            Username = "test",
            Password = "password"
        };
        var creator = new InMemoryUserCreator();

        await creator.CreateUser(user);
        var result = await creator.CreateUser(user);

        Assert.IsFalse(result.SaveSuccessful);
        Assert.That(result.ErrorMessage, Contains.Substring("already exists"));
    }
}