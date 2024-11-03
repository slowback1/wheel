using Common.Data;
using Common.Interfaces;
using Data.InMemory;
using UseCases.User;

namespace Dsl;

public abstract class ManagingUsersDsl
{
    private const string DefaultUsername = "Username";
    private const string DefaultPassword = "Password!1";

    private string CurrentLoggedInHash { get; set; } = string.Empty;
    private Exception? LastError { get; set; }
    protected IDataAccess DataAccess { get; set; } = new InMemoryDataAccess();

    public async Task Register(string username = DefaultUsername, string password = DefaultPassword)
    {
        var user = new CreateUser
        {
            Password = password,
            Username = username
        };

        var useCase = new RegisterUserUseCase(DataAccess);

        var result = await useCase.Register(user);

        if (result.Status == FeatureResultStatus.Ok)
            CurrentLoggedInHash = result.Data!;
        if (result.Status == FeatureResultStatus.Error)
            LastError = result.Exception;
    }

    public void AssertIsLoggedIn()
    {
        Assert.That(CurrentLoggedInHash, Is.Not.Empty);
    }

    public void AssertLastErrorIs(string message)
    {
        Assert.That(LastError, Is.Not.Null);
        Assert.That(LastError!.Message, Contains.Substring(message));
    }
}