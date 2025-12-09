using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;

namespace Data.InMemory;

internal class InMemoryIntervalCreator : IIntervalCreator
{
    public Task<SaveResult<Interval>> CreateInterval(CreateInterval interval)
    {
        // In-memory implementation not required for this feature
        return Task.FromResult(SaveResult<Interval>.Failure("In-memory interval storage not implemented"));
    }
}

internal class InMemoryIntervalRetriever : IIntervalRetriever
{
    public Task<Interval?> GetInterval(string username, string intervalName)
    {
        return Task.FromResult<Interval?>(null);
    }

    public Task<IEnumerable<Interval>> GetIntervalsForUser(string username)
    {
        return Task.FromResult(Enumerable.Empty<Interval>());
    }

    public Task<IEnumerable<Interval>> GetIntervalsDueForExecution()
    {
        return Task.FromResult(Enumerable.Empty<Interval>());
    }
}

internal class InMemoryIntervalDeleter : IIntervalDeleter
{
    public Task<SaveResult<Interval>> DeleteInterval(string username, string intervalName)
    {
        return Task.FromResult(SaveResult<Interval>.Failure("In-memory interval storage not implemented"));
    }
}

internal class InMemoryIntervalUpdater : IIntervalUpdater
{
    public Task<SaveResult<Interval>> UpdateIntervalLastRun(string username, string intervalName, DateTime lastRunTime, DateTime nextRunTime)
    {
        return Task.FromResult(SaveResult<Interval>.Failure("In-memory interval storage not implemented"));
    }
}
