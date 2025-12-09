using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileIntervalRetriever : FileRepository, IIntervalRetriever
{
    public FileIntervalRetriever(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<Interval?> GetInterval(string username, string intervalName)
    {
        var interval = Intervals
            .FirstOrDefault(i => i.Username == username && i.Name == intervalName);

        return Task.FromResult(interval?.ToInterval());
    }

    public Task<IEnumerable<Interval>> GetIntervalsForUser(string username)
    {
        var intervals = Intervals
            .Where(i => i.Username == username)
            .ToIntervals();

        return Task.FromResult(intervals);
    }

    public Task<IEnumerable<Interval>> GetIntervalsDueForExecution()
    {
        var now = DateTime.UtcNow;
        var dueIntervals = Intervals
            .Where(i => i.NextRunTime <= now)
            .ToIntervals();

        return Task.FromResult(dueIntervals);
    }
}
