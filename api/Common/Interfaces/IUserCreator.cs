using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IUserCreator
{
    Task<SaveResult<User>> CreateUser(CreateUser user);
}