using Common.Data;
using Data.File.Mappers;
using Data.Tests.TestUtilities;
using TestUtilities.TestData;

namespace Data.Tests.File.Mappers;

public class FileWheelMapperTests
{
    [Test]
    public void WheelSetting_ToFileWheel_MapsCorrectly()
    {
        WheelSetting wheelSetting = TestWheelSettings.GetTestWheelSetting();

        var result = wheelSetting.ToFileWheel();

        Assert.That(result.Name, Is.EqualTo(wheelSetting.Name));
        Assert.That(result.Username, Is.Null);

        Assert.That(result.Slices.Count(), Is.EqualTo(wheelSetting.Slices.Count()));
    }

    [Test]
    public void FileWheel_ToWheelSetting_MapsCorrectly()
    {
        var fileWheel = TestFileModels.GetTestFileWheel();

        var result = fileWheel.ToWheelSetting();

        Assert.That(result.Name, Is.EqualTo(fileWheel.Name));
        Assert.That(result.Slices.Count(), Is.EqualTo(fileWheel.Slices.Count()));
    }

    [Test]
    public void CreateWheelSetting_ToFileWheel_MapsCorrectly()
    {
        var wheel = TestWheelSettings.GetTestWheelSetting();

        var result = wheel.ToFileWheel();

        Assert.That(result.Name, Is.EqualTo(wheel.Name));
        Assert.That(result.Username, Is.EqualTo(wheel.Username));
        Assert.That(result.Slices.Count(), Is.EqualTo(wheel.Slices.Count()));
    }

    [Test]
    public void FileWheelSlice_ToWheelSlice_MapsCorrectly()
    {
        var fileWheelSlice = TestFileModels.GetTestFileWheelSlice();

        var result = fileWheelSlice.ToWheelSlice();

        Assert.That(result.Label, Is.EqualTo(fileWheelSlice.Label));
        Assert.That(result.Size, Is.EqualTo(fileWheelSlice.Size));
    }

    [Test]
    public void WheelSlice_ToFileWheelSlice_MapsCorrectly()
    {
        var wheelSlice = TestWheelSettings.GetTestWheelSlice();

        var result = wheelSlice.ToFileWheelSlice();

        Assert.That(result.Label, Is.EqualTo(wheelSlice.Label));
        Assert.That(result.Size, Is.EqualTo(wheelSlice.Size));
    }
}