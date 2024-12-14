using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using Data.File.Mappers;
using Data.File.Store;

namespace Data.File;

internal class FileWheelCreator : FileRepository, IWheelCreator
{
    public FileWheelCreator(IFileStoreRetriever retriever) : base(retriever)
    {
    }

    public async Task<SaveResult<WheelSetting>> CreateWheelSetting(CreateWheelSetting wheelSetting)
    {
        var entity = wheelSetting.ToFileWheel();

        var existingWheel = Wheels.FirstOrDefault(w => w.Name == entity.Name && w.Username == entity.Username);

        if (existingWheel != null) return SaveResult<WheelSetting>.Failure("Wheel setting already exists");

        Wheels.Add(entity);

        try
        {
            SaveChanges();

            return SaveResult<WheelSetting>.Success(entity.ToWheelSetting());
        }
        catch (Exception e)
        {
            return SaveResult<WheelSetting>.Failure(e.Message);
        }
    }
}