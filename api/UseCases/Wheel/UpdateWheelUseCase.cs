using System.Threading.Tasks;
using Common.Data;
using Common.Interfaces;
using UseCases.Shared;

namespace UseCases.Wheel;

public class UpdateWheelUseCase : DataAccessorUseCase
{
    public UpdateWheelUseCase(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    public async Task<FeatureResult<WheelSetting>> UpdateWheelSetting(string wheelId, WheelSetting wheelSetting)
    {
        var stored = await _dataAccess.WheelRetriever.GetWheelSetting(wheelId);

        if (stored is null) return FeatureResult<WheelSetting>.NotFound();

        stored.Name = wheelSetting.Name;
        stored.Slices = wheelSetting.Slices;

        var result = await _dataAccess.WheelUpdater.UpdateWheelSetting(stored);

        return FeatureResult<WheelSetting>.Ok(result.SavedEntity!);
    }
}