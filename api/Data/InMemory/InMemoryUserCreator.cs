using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.InMemory.Models;

namespace Data.InMemory;

public class InMemoryUserCreator : IUserCreator
{
    public async Task<SaveResult<User>> CreateUser(CreateUser user)
    {
        if (UserExists(user.Username))
            return ErrorUserExists(user.Username);

        SaveUser(user);

        return SaveResult<User>.Success(new User { Username = user.Username });
    }

    private void SaveUser(CreateUser user)
    {
        InMemoryStore.Users.Add(new InMemoryUser
        {
            Username = user.Username,
            PasswordHash = user.Password
        });
    }

    private bool UserExists(string username)
    {
        return InMemoryStore.Users.Exists(u => u.Username == username);
    }

    private SaveResult<User> ErrorUserExists(string username)
    {
        return SaveResult<User>.Failure($"User with username {username} already exists");
    }
}