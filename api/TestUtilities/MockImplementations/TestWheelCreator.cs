using Common.Data;
using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestWheelCreator : IWheelCreator
{
    public static readonly string NameThatFails = "Failure";
    public static readonly string NameThatErrors = "Error";
    public static WheelSetting? LastCreatedWheelSetting { get; private set; }

    public async Task<SaveResult<WheelSetting>> CreateWheelSetting(WheelSetting wheelSetting)
    {
        if (wheelSetting.Name == NameThatFails) return SaveResult<WheelSetting>.Failure("Failed to create wheel");
        if (wheelSetting.Name == NameThatErrors) throw new Exception("Error");

        LastCreatedWheelSetting = wheelSetting;

        return SaveResult<WheelSetting>.Success(wheelSetting);
    }
}