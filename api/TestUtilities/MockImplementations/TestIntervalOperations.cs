using Common.Data;
using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestIntervalCreator : IIntervalCreator
{
    public Task<SaveResult<Interval>> CreateInterval(CreateInterval interval)
    {
        return Task.FromResult(SaveResult<Interval>.Success(new Interval
        {
            Username = interval.Username,
            Frequency = interval.Frequency,
            Name = interval.Name,
            PresetName = interval.PresetName
        }));
    }
}

public class TestIntervalRetriever : IIntervalRetriever
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

public class TestIntervalDeleter : IIntervalDeleter
{
    public Task<SaveResult<Interval>> DeleteInterval(string username, string intervalName)
    {
        return Task.FromResult(SaveResult<Interval>.Failure("Test interval storage not implemented"));
    }
}

public class TestIntervalUpdater : IIntervalUpdater
{
    public Task<SaveResult<Interval>> UpdateIntervalLastRun(string username, string intervalName, DateTime lastRunTime,
        DateTime nextRunTime)
    {
        return Task.FromResult(SaveResult<Interval>.Failure("Test interval storage not implemented"));
    }
}