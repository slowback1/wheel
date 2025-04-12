using Data.InMemory;
using Data.InMemory.Models;

namespace Data.Tests.InMemory;

public class InMemoryUserRetrieverTests
{
    private const string TestUsername = "TestUser";

    [SetUp]
    public void SetUpTestUser()
    {
        InMemoryStore.Users.Add(new InMemoryUser
        {
            Username = TestUsername,
            PasswordHash = "password"
        });
    }

    [Test]
    public async Task GetUser_WhenUserExists_ReturnUser()
    {
        var retriever = new InMemoryUserRetriever();

        var user = await retriever.GetUser(TestUsername);

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Username, Is.EqualTo(TestUsername));
    }

    [Test]
    public async Task GetUser_WhenUserDoesNotExist_ReturnNull()
    {
        var retriever = new InMemoryUserRetriever();

        var user = await retriever.GetUser("NonExistentUser");

        Assert.That(user, Is.Null);
    }

    [Test]
    public async Task GetPasswordHash_WhenUserExists_ReturnPasswordHash()
    {
        var retriever = new InMemoryUserRetriever();

        var passwordHash = await retriever.GetPasswordHash(TestUsername);

        Assert.That(passwordHash, Is.EqualTo("password"));
    }

    [Test]
    public async Task GetPasswordHash_WhenUserDoesNotExist_ReturnNull()
    {
        var retriever = new InMemoryUserRetriever();

        var passwordHash = await retriever.GetPasswordHash("NonExistentUser");

        Assert.That(passwordHash, Is.Null);
    }
}