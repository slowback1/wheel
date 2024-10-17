using Data.InMemory;
using TestUtilities.TestData;

namespace Data.Tests.InMemory;

public class InMemoryWheelCreatorTests
{
    [SetUp]
    public void ClearInMemoryStore()
    {
        InMemoryWheelStore.Wheels.Clear();
    }

    [Test]
    public async Task CreateReturnsSetting()
    {
        var creator = new InMemoryWheelCreator();

        var setting = TestWheelSettings.GetTestWheelSetting();

        var result = await creator.CreateWheelSetting(setting);

        Assert.That(result.SaveSuccessful, Is.True);
        Assert.That(result.SavedEntity, Is.EqualTo(setting));
    }

    [Test]
    public async Task CreateCreatesTheSettingInTheInMemoryStore()
    {
        var creator = new InMemoryWheelCreator();
        var setting = TestWheelSettings.GetTestWheelSetting();
        await creator.CreateWheelSetting(setting);

        var stored = InMemoryWheelStore.Wheels.First();

        Assert.That(stored.Name, Is.EqualTo(setting.Name));
        Assert.That(stored.Slices.Count(), Is.EqualTo(1));
        Assert.That(stored.Slices.First().Label, Is.EqualTo("Label"));
        Assert.That(stored.Slices.First().Size, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateReturnsErrorIfNameIsAlreadyUsed()
    {
        var creator = new InMemoryWheelCreator();
        var setting = TestWheelSettings.GetTestWheelSetting();
        await creator.CreateWheelSetting(setting);
        var result = await creator.CreateWheelSetting(setting);

        Assert.That(result.SaveSuccessful, Is.False);
        Assert.That(result.ErrorMessage, Is.EqualTo("A wheel with that name 'Name' already exists."));
    }
}