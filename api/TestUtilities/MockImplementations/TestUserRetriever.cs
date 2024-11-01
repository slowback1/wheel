using Common.Data;
using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestUserRetriever : IUserRetriever
{
    public const string NotFoundUsername = "not-found-user";

    public Task<User?> GetUser(string username)
    {
        if (username == NotFoundUsername) return Task.FromResult<User?>(null);

        return Task.FromResult(new User { Username = username });
    }

    public Task<string?> GetPasswordHash(string username)
    {
        if (username == NotFoundUsername) return Task.FromResult<string?>(null);

        return Task.FromResult("password-hash");
    }
}