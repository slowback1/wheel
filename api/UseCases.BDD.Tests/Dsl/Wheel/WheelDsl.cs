using Common.Data;
using Common.Interfaces;
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

    public WheelSetting? LastLoadedWheel { get; set; }
    public IEnumerable<WheelSetting>? LastLoadedWheels { get; set; }

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

        await CreateWheelSettingUseCase.CreateWheelSetting(wheel);

        return wheel;
    }

    public async Task<WheelSetting?> GetWheel(string name)
    {
        return (await GetWheelSettingUseCase.GetWheelSetting(name)).Data;
    }

    public async Task AssertWheelExists(string name)
    {
        var wheel = await GetWheel(name);

        Assert.That(wheel, Is.Not.Null);
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
        LastLoadedWheels = (await GetWheelSettingsUseCase.GetWheelSettings()).Data;
    }
}