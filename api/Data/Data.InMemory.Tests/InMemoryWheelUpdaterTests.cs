using Common.Data;
using Data.InMemory;
using TestUtilities.TestData;

namespace Data.Tests.InMemory;

public class InMemoryWheelUpdaterTests
{
    private WheelSetting WheelSetting { get; set; }

    [SetUp]
    public async Task CreateTestWheelSetting()
    {
        InMemoryStore.Wheels.Clear();

        var wheel = TestWheelSettings.GetTestWheelSetting();

        var result = await new InMemoryWheelCreator().CreateWheelSetting(wheel);

        WheelSetting = result.SavedEntity!;
    }

    [Test]
    public async Task UpdateWheelSetting_ValidData_ReturnsSuccess()
    {
        WheelSetting.Name = "Updated Wheel Name";
        WheelSetting.Slices.First().Label = "Updated Slice Label";
        WheelSetting.Slices.First().Size = 5;

        var result = await new InMemoryWheelUpdater().UpdateWheelSetting(WheelSetting);

        Assert.That(result.SaveSuccessful, Is.True);
        Assert.That(result.SavedEntity, Is.Not.Null);
        Assert.That(result.SavedEntity!.Name, Is.EqualTo("Updated Wheel Name"));
        Assert.That(result.SavedEntity.Slices.First().Label, Is.EqualTo("Updated Slice Label"));
        Assert.That(result.SavedEntity.Slices.First().Size, Is.EqualTo(5));
    }

    [Test]
    public async Task UpdateWheelSetting_ReturnsFailure_ForWheelThatDoesNotExist()
    {
        var wheelSetting = new WheelSetting
        {
            Name = "Nonexistent Wheel",
            Slices = new List<WheelSlice>
            {
                new()
                {
                    Label = "Slice 1",
                    Size = 1
                }
            }
        };

        var result = await new InMemoryWheelUpdater().UpdateWheelSetting(wheelSetting);

        Assert.That(result.SaveSuccessful, Is.False);
        Assert.That(result.SavedEntity, Is.Null);
        Assert.That(result.ErrorMessage, Is.EqualTo("Wheel does not exist"));
    }
}