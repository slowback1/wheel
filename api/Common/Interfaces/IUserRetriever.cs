using System.Threading.Tasks;
using Common.Data;

namespace Common.Interfaces;

public interface IUserRetriever
{
    Task<User?> GetUser(string username);
    Task<string?> GetPasswordHash(string username);
}