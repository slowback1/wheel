using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileUserCreator : FileRepository, IUserCreator
{
    public FileUserCreator(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public async Task<SaveResult<User>> CreateUser(CreateUser user)
    {
        var userExists = Users.Any(u => u.Username == user.Username);

        if (userExists) return SaveResult<User>.Failure("User already exists");

        var entity = user.ToFileUser();

        Users.Add(entity);

        SaveChanges();

        return SaveResult<User>.Success(entity.ToUser());
    }
}