using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Models;
using Data.File.Store;

namespace Data.File;

internal class FileWheelUpdater : FileRepository, IWheelUpdater
{
    public FileWheelUpdater(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public async Task<SaveResult<WheelSetting>> UpdateWheelSetting(WheelSetting wheelSetting)
    {
        var stored = await GetFileWheel(wheelSetting);

        stored.Name = wheelSetting.Name;
        stored.Slices = wheelSetting.Slices.Select(x => x.ToFileWheelSlice()).ToList();

        SaveChanges();

        return SaveResult<WheelSetting>.Success(stored.ToWheelSetting());
    }

    private async Task<FileWheel?> GetFileWheel(WheelSetting wheelSetting)
    {
        Load();

        var fileWheel = Wheels.FirstOrDefault(w => w.Name == wheelSetting.Name);

        return fileWheel;
    }
}