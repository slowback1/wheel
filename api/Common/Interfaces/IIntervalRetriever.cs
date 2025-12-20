using Common.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces;

public interface IIntervalRetriever
{
    Task<Interval?> GetInterval(string username, string intervalName);
    Task<IEnumerable<Interval>> GetIntervalsForUser(string username);
    Task<IEnumerable<Interval>> GetIntervalsDueForExecution();
}
