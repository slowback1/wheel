using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileWheelRetriever : FileRepository, IWheelRetriever
{
    public FileWheelRetriever(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public async Task<WheelSetting?> GetWheelSetting(string wheelId)
    {
        return Wheels.FirstOrDefault(w => w.Name == wheelId)?.ToWheelSetting();
    }

    public async Task<IEnumerable<WheelSetting>> GetWheelSettings(string username)
    {
        return Wheels
            .Where(w => w.Username == username)
            .Select(w => w.ToWheelSetting())
            .ToList();
    }
}