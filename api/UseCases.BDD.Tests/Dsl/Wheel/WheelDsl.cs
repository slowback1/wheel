using Common.Data;
using Common.Interfaces;
using TestUtilities.MockImplementations;
using UseCases.Wheel;

namespace UseCases.BDD.Tests.Dsl;

public abstract class WheelDsl
{
    protected WheelDsl(IDataAccess dataAccess)
    {
        CreateWheelSettingUseCase = new CreateWheelSettingUseCase(dataAccess);
        GetWheelSettingUseCase = new GetWheelSettingUseCase(dataAccess);
        GetWheelSettingsUseCase = new GetWheelSettingsUseCase(dataAccess);

        SetupData().Wait();
    }

    public string FirstWheelName { get; set; } = "Test Wheel";
    public string SecondWheelName { get; set; } = "Test Wheel 2";
    public string NonExistantWheelName { get; set; } = "Non Existant Wheel";

    public FeatureResult<WheelSetting>? LastLoadedWheel { get; set; }
    public FeatureResult<WheelSetting>? LastCreatedWheel { get; set; }
    public FeatureResult<IEnumerable<WheelSetting>>? LastLoadedWheels { get; set; }

    protected CreateWheelSettingUseCase CreateWheelSettingUseCase { get; set; }
    protected GetWheelSettingUseCase GetWheelSettingUseCase { get; set; }
    protected GetWheelSettingsUseCase GetWheelSettingsUseCase { get; set; }

    protected abstract Task SetupData();

    public async Task<WheelSetting> CreateWheel(string name)
    {
        var wheel = new WheelSetting
        {
            Name = name,

            Slices = new[] { new WheelSlice { Label = "Label", Size = 1 } }
        };

        var result = await CreateWheelSettingUseCase.CreateWheelSetting(wheel);

        LastCreatedWheel = result;

        return wheel;
    }

    public void CauseGetWheelsToError()
    {
        var dataAccess = new TestDataAccess();
        var retriever = dataAccess.WheelRetriever as TestWheelRetriever;
        retriever!.SetShouldThrowWhenGettingAllSettings(true);

        GetWheelSettingsUseCase = new GetWheelSettingsUseCase(dataAccess);
    }

    public async Task<FeatureResult<WheelSetting>> GetWheel(string name)
    {
        return await GetWheelSettingUseCase.GetWheelSetting(name);
    }

    public async Task AssertWheelExists(string name)
    {
        var wheel = await GetWheel(name);

        Assert.That(wheel.Data, Is.Not.Null);
    }

    public async Task AssertThereIsAnyWheelData()
    {
        var wheels = await GetWheelSettingsUseCase.GetWheelSettings();
        Assert.That(wheels.Data, Is.Not.Null);
        Assert.That(wheels.Data.Count(), Is.GreaterThan(0));
    }

    public async Task LoadWheel(string name)
    {
        LastLoadedWheel = await GetWheel(name);
    }

    public async Task LoadWheels()
    {
        LastLoadedWheels = await GetWheelSettingsUseCase.GetWheelSettings();
    }
}