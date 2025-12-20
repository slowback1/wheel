using Common.Data;
using System.Threading.Tasks;

namespace Common.Interfaces;

public interface IIntervalCreator
{
    Task<SaveResult<Interval>> CreateInterval(CreateInterval interval);
}
