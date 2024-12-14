using Data.File.Mappers;
using Data.Tests.TestUtilities;
using TestUtilities.TestData;

namespace Data.Tests.File.Mappers;

public class FileUserMappersTests
{
    [Test]
    public void CreateUser_ToFileUser_MapsCorrectly()
    {
        var user = TestUsers.GetTestCreateUser();

        var result = user.ToFileUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.PasswordHash, Is.EqualTo(user.Password));
    }

    [Test]
    public void FileUser_ToCreateUser_MapsCorrectly()
    {
        var user = TestFileModels.GetTestFileUser();

        var result = user.ToCreateUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.Password, Is.EqualTo(user.PasswordHash));
    }

    [Test]
    public void LoginUser_ToFileUser_MapsCorrectly()
    {
        var user = TestUsers.GetTestLoginUser();

        var result = user.ToFileUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.PasswordHash, Is.EqualTo(user.Password));
    }

    [Test]
    public void FileUser_ToLoginUser_MapsCorrectly()
    {
        var user = TestFileModels.GetTestFileUser();

        var result = user.ToLoginUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.Password, Is.EqualTo(user.PasswordHash));
    }

    [Test]
    public void FileUser_ToUser_MapsCorrectly()
    {
        var user = TestFileModels.GetTestFileUser();

        var result = user.ToUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
    }

    [Test]
    public void User_ToFileUser_MapsCorrectly()
    {
        var user = TestUsers.GetTestUser();

        var result = user.ToFileUser();

        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.PasswordHash, Is.Null);
    }
}