using Common.Data;
using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestUserCreator : IUserCreator
{
    public const string ErrorUsername = "errored-user";

    public async Task<SaveResult<User>> CreateUser(CreateUser user)
    {
        if (user.Username == ErrorUsername) return SaveResult<User>.Failure("Error creating user");

        return SaveResult<User>.Success(new User { Username = user.Username });
    }
}