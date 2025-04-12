using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.InMemory.Models;

namespace Data.InMemory;

public class InMemoryUserRetriever : IUserRetriever
{
    public async Task<User?> GetUser(string username)
    {
        var user = GetUserByName(username);

        if (user == null) return null;

        return new User { Username = user.Username };
    }

    public async Task<string?> GetPasswordHash(string username)
    {
        var user = GetUserByName(username);

        if (user == null) return null;

        return user.PasswordHash;
    }

    private InMemoryUser? GetUserByName(string username)
    {
        return InMemoryStore.Users.Find(u => u.Username == username);
    }
}