using Data.File;
using Data.Tests.File.Store;
using Data.Tests.TestUtilities;
using TestUtilities.TestData;

namespace Data.Tests.File;

public class FileWheelCreatorTests
{
    [Test]
    public async Task CreateWheelSetting_WhenCalled_IndicatesSaveSuccessful()
    {
        var setting = TestWheelSettings.GetTestWheelSetting();
        var store = new TestFileStoreRetriever();
        var creator = new FileWheelCreator(store);

        var result = await creator.CreateWheelSetting(setting);

        Assert.That(result.SaveSuccessful, Is.True);
    }

    [Test]
    public async Task CreateWheelSetting_WhenCalled_SavesWheelSettingToStore()
    {
        var setting = TestWheelSettings.GetTestWheelSetting();
        var store = new TestFileStoreRetriever();
        var creator = new FileWheelCreator(store);

        var result = await creator.CreateWheelSetting(setting);

        Assert.That(store.WheelsToGet.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task CreateWheelSetting_WhenCalled_IndicatesSaveFailedWhenDuplicate()
    {
        var setting = TestWheelSettings.GetTestWheelSetting();
        var store = new TestFileStoreRetriever();
        store.WheelsToGet = new[]
        {
            TestFileModels.GetTestFileWheel()
        };

        var existingWheel = store.WheelsToGet.First();

        setting.Name = existingWheel.Name;
        setting.Username = existingWheel.Username;

        var creator = new FileWheelCreator(store);

        var result = await creator.CreateWheelSetting(setting);

        Assert.That(result.SaveSuccessful, Is.False);
        Assert.That(result.ErrorMessage, Is.EqualTo("Wheel setting already exists"));
    }

    [Test]
    public async Task CreateWheelSetting_WhenCalled_ReturnsSavedEntity()
    {
        var setting = TestWheelSettings.GetTestWheelSetting();
        var store = new TestFileStoreRetriever();
        var creator = new FileWheelCreator(store);

        var result = await creator.CreateWheelSetting(setting);

        Assert.That(result.SavedEntity, Is.Not.Null);
        Assert.That(result.SavedEntity.Name, Is.EqualTo(setting.Name));
    }
}