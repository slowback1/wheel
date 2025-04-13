using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Models;
using Data.File.Store;

namespace Data.File;

internal class FileUserRetriever : FileRepository, IUserRetriever
{
    public FileUserRetriever(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public async Task<User?> GetUser(string username)
    {
        Load();
        var stored = GetFileUser(username);

        return stored?.ToUser();
    }

    public async Task<string?> GetPasswordHash(string username)
    {
        var stored = GetFileUser(username);

        return stored?.PasswordHash;
    }

    private FileUser? GetFileUser(string username)
    {
        return Users.FirstOrDefault(u => u.Username == username);
    }
}