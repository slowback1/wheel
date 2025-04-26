using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileWheelDeleter : FileRepository, IWheelDeleter
{
    public FileWheelDeleter(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public Task<SaveResult<WheelSetting>> DeleteWheelSetting(string wheelId)
    {
        var storedWheel = Wheels.Find(x => x.Name == wheelId);
        if (storedWheel == null) return Task.FromResult(SaveResult<WheelSetting>.Failure("Wheel not found"));

        Wheels.Remove(storedWheel);

        SaveChanges();

        return Task.FromResult(SaveResult<WheelSetting>.Success(storedWheel.ToWheelSetting()));
    }
}