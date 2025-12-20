using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileIntervalCreator : FileRepository, IIntervalCreator
{
    public FileIntervalCreator(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<SaveResult<Interval>> CreateInterval(CreateInterval interval)
    {
        var exists = Intervals.Any(i => 
            i.Username == interval.Username && i.Name == interval.Name);

        if (exists)
        {
            return Task.FromResult(SaveResult<Interval>.Failure("Interval with this name already exists."));
        }

        var fileInterval = interval.ToFileInterval();
        Intervals.Add(fileInterval);
        SaveChanges();

        return Task.FromResult(SaveResult<Interval>.Success(fileInterval.ToInterval()));
    }
}
