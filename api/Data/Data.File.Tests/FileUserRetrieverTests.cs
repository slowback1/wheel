using Data.File;
using Data.File.Store;
using Data.Tests.File.Store;
using TestUtilities.TestData;

namespace Data.Tests.File;

public class FileUserRetrieverTests
{
    private const string NonExistingUsername = "NonExistingUsername";
    private string Username { get; set; }
    private IFileStoreRetriever FileStoreRetriever { get; set; }


    [SetUp]
    public async Task SetUp()
    {
        FileStoreRetriever = new TestFileStoreRetriever();
        var user = TestUsers.GetTestCreateUser();
        var creator = new FileUserCreator(FileStoreRetriever);
        var createdUser = await creator.CreateUser(user);
        Username = createdUser.SavedEntity!.Username;
    }

    [Test]
    public async Task GetUser_WhenCalled_ReturnsUser()
    {
        var retriever = new FileUserRetriever(FileStoreRetriever);

        var result = await retriever.GetUser(Username);

        Assert.That(result, Is.Not.Null);

        Assert.That(result!.Username, Is.EqualTo(Username));
    }

    [Test]
    public async Task GetUser_WhenUserDoesNotExist_ReturnsNull()
    {
        var retriever = new FileUserRetriever(FileStoreRetriever);

        var result = await retriever.GetUser(NonExistingUsername);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetPasswordHash_WhenCalled_ReturnsPasswordHash()
    {
        var retriever = new FileUserRetriever(FileStoreRetriever);

        var result = await retriever.GetPasswordHash(Username);

        Assert.That(result, Is.Not.Null);
        Assert.That(string.IsNullOrEmpty(result), Is.False);
    }

    [Test]
    public async Task GetPasswordHash_WhenUserDoesNotExist_ReturnsNull()
    {
        var retriever = new FileUserRetriever(FileStoreRetriever);

        var result = await retriever.GetPasswordHash(NonExistingUsername);

        Assert.That(result, Is.Null);
    }
}