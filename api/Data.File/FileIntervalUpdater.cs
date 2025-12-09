using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileIntervalUpdater : FileRepository, IIntervalUpdater
{
    public FileIntervalUpdater(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<SaveResult<Interval>> UpdateIntervalLastRun(string username, string intervalName, DateTime lastRunTime, DateTime nextRunTime)
    {
        var interval = Intervals.FirstOrDefault(i => 
            i.Username == username && i.Name == intervalName);

        if (interval == null)
        {
            return Task.FromResult(SaveResult<Interval>.Failure("Interval not found."));
        }

        interval.LastRunTime = lastRunTime;
        interval.NextRunTime = nextRunTime;
        SaveChanges();

        return Task.FromResult(SaveResult<Interval>.Success(interval.ToInterval()));
    }
}
