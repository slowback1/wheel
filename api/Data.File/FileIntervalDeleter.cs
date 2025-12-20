using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileIntervalDeleter : FileRepository, IIntervalDeleter
{
    public FileIntervalDeleter(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<SaveResult<Interval>> DeleteInterval(string username, string intervalName)
    {
        var interval = Intervals.FirstOrDefault(i => 
            i.Username == username && i.Name == intervalName);

        if (interval == null)
        {
            return Task.FromResult(SaveResult<Interval>.Failure("Interval not found."));
        }

        var deletedInterval = interval.ToInterval();
        Intervals.Remove(interval);
        SaveChanges();

        return Task.FromResult(SaveResult<Interval>.Success(deletedInterval));
    }
}
