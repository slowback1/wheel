using Common.Data;
using Data.InMemory;

namespace Data.Tests.InMemory;

public class InMemoryWheelRetrieverTests
{
    private WheelSetting _wheelSetting { get; set; }
    private InMemoryWheelRetriever _inMemoryWheelRetriever { get; } = new();

    [SetUp]
    public void AddAWheelItem()
    {
        _wheelSetting = new CreateWheelSetting
        {
            Name = "Test Wheel",
            Slices = new[]
            {
                new WheelSlice
                {
                    Label = "Test Slice",
                    Size = 1
                }
            },
            Username = "test"
        };

        InMemoryStore.Wheels.Clear();
        InMemoryStore.Wheels.Add("test", new List<WheelSetting>());

        InMemoryStore.Wheels["test"].Add(_wheelSetting);
    }

    [Test]
    public async Task ReturnsTheWheelSettingsWhenGettingAllWheelSettings()
    {
        var wheels = await _inMemoryWheelRetriever.GetWheelSettings("test");

        Assert.That(wheels, Is.EquivalentTo(InMemoryStore.Wheels["test"]));
    }

    [Test]
    public async Task ReturnsTheWheelSettingWhenGivenAName()
    {
        var wheel = await _inMemoryWheelRetriever.GetWheelSetting(_wheelSetting.Name);

        Assert.That(wheel.Name, Is.EqualTo(_wheelSetting.Name));
        Assert.That(wheel.Slices.Count(), Is.EqualTo(_wheelSetting.Slices.Count()));
    }

    [Test]
    public async Task ReturnsNullWhenGivenAnUnknownName()
    {
        var wheel = await _inMemoryWheelRetriever.GetWheelSetting("Unknown Wheel");

        Assert.That(wheel, Is.Null);
    }
}