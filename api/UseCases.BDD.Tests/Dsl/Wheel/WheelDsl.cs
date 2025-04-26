using Common.Data;
using Common.Interfaces;
using TestUtilities.MockImplementations;
using UseCases.BDD.Tests.Dsl.Users;
using UseCases.Wheel;

namespace UseCases.BDD.Tests.Dsl;

public abstract class WheelDsl
{
    protected WheelDsl(IDataAccess dataAccess)
    {
        CreateWheelSettingUseCase = new CreateWheelSettingUseCase(dataAccess);
        GetWheelSettingUseCase = new GetWheelSettingUseCase(dataAccess);
        GetWheelSettingsUseCase = new GetWheelSettingsUseCase(dataAccess);
        UpdateWheelUseCase = new UpdateWheelUseCase(dataAccess);
        ManagingUsersDsl = new ManagingUsersNotCreatedYetDsl();
        DeleteWheelSettingUseCase = new DeleteWheelSettingUseCase(dataAccess);

        CreateUsers().Wait();
        SetupData().Wait();
    }

    public ManagingUsersDsl ManagingUsersDsl { get; set; }

    public string FirstWheelName { get; set; } = "Test Wheel";
    public string SecondWheelName { get; set; } = "Test Wheel 2";
    public string NonExistantWheelName { get; set; } = "Non Existant Wheel";
    public string Username { get; set; } = "test";
    public string OtherUsername { get; set; } = "other";

    public string FirstLoggedInUserToken { get; set; }
    public string SecondLoggedInUserHash { get; set; }

    public FeatureResult<WheelSetting>? LastLoadedWheel { get; set; }
    public FeatureResult<WheelSetting>? LastCreatedWheel { get; set; }
    public FeatureResult<IEnumerable<WheelSetting>>? LastLoadedWheels { get; set; }
    public FeatureResult<bool>? LastDeletedWheelResult { get; set; }

    protected CreateWheelSettingUseCase CreateWheelSettingUseCase { get; set; }
    protected GetWheelSettingUseCase GetWheelSettingUseCase { get; set; }
    protected UpdateWheelUseCase UpdateWheelUseCase { get; set; }
    protected GetWheelSettingsUseCase GetWheelSettingsUseCase { get; set; }
    protected DeleteWheelSettingUseCase DeleteWheelSettingUseCase { get; set; }

    private async Task CreateUsers()
    {
        await ManagingUsersDsl.Register(Username, "test!123ACd");
        FirstLoggedInUserToken = ManagingUsersDsl.CurrentLoggedInHash;
        await ManagingUsersDsl.Register(OtherUsername, "test!123ABd");
        SecondLoggedInUserHash = ManagingUsersDsl.CurrentLoggedInHash;
    }

    protected abstract Task SetupData();

    public async Task<WheelSetting> CreateWheel(string name, string? userHash = null)
    {
        var tokenToUse = userHash ?? FirstLoggedInUserToken;

        var wheel = new CreateWheelSetting
        {
            Name = name,
            Slices = new[] { new WheelSlice { Label = "Label", Size = 1 } }
        };

        var result = await CreateWheelSettingUseCase.CreateWheelSetting(wheel, tokenToUse);

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

    public async Task<FeatureResult<WheelSetting>> UpdateWheel(WheelSetting setting)
    {
        var result = await UpdateWheelUseCase.UpdateWheelSetting(setting.Name, setting);

        LastCreatedWheel = result;
        LastLoadedWheel = result;

        return result;
    }

    public async Task AssertWheelExists(string name)
    {
        var wheel = await GetWheel(name);

        Assert.That(wheel.Data, Is.Not.Null);
    }

    public async Task AssertThereIsAnyWheelData()
    {
        var wheels = await GetWheelSettingsUseCase.GetWheelSettings(FirstLoggedInUserToken);
        Assert.That(wheels.Data, Is.Not.Null);
        Assert.That(wheels.Data.Count(), Is.GreaterThan(0));
    }

    public async Task LoadWheel(string name)
    {
        LastLoadedWheel = await GetWheel(name);
    }

    public async Task LoadWheels(string? token = null)
    {
        var tokenToUse = token ?? FirstLoggedInUserToken;

        LastLoadedWheels = await GetWheelSettingsUseCase.GetWheelSettings(tokenToUse);
    }

    public void AssertWheelDoesNotExist(string name)
    {
        var wheels = LastLoadedWheels!.Data;

        var wheel = wheels.FirstOrDefault(w => w.Name == name);

        Assert.That(wheel, Is.Null);
    }

    public void AssertCreationErrorMessageIs(string message)
    {
        var lastError = LastCreatedWheel?.Exception;

        Assert.That(lastError, Is.Not.Null);

        Assert.That(lastError!.Message, Contains.Substring(message));
    }

    public void AssertViewErrorMessageIs(string message)
    {
        var lastError = LastLoadedWheels?.Exception;

        Assert.That(lastError, Is.Not.Null);

        Assert.That(lastError!.Message, Contains.Substring(message));
    }

    public async Task DeleteWheel(string name)
    {
        var result = await DeleteWheelSettingUseCase.DeleteWheelSetting(name);

        LastDeletedWheelResult = result;
    }
}