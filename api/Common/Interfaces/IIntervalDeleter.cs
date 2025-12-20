using Common.Data;
using System.Threading.Tasks;

namespace Common.Interfaces;

public interface IIntervalDeleter
{
    Task<SaveResult<Interval>> DeleteInterval(string username, string intervalName);
}
