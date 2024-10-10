namespace Data.Tests;

public class WheelRetrieverTests
{
    [Test]
    public async Task CanGetAWheelSettingById()
    {
        var retriever = new WheelRetriever();
        var result = await retriever.GetWheelSetting("Wheel 1");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Wheel 1"));
        Assert.That(result.Slices.Count(), Is.EqualTo(4));

        Assert.That(result.Slices.First().Label, Is.EqualTo("Slice 1"));
        Assert.That(result.Slices.First().Size, Is.EqualTo(1));
    }

    [Test]
    public async Task ReturnsNullIfWheelSettingNotFound()
    {
        var retriever = new WheelRetriever();
        var result = await retriever.GetWheelSetting("Wheel 4");

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetWheelSettingsReturnsThreeWheelSettings()
    {
        var retriever = new WheelRetriever();
        var result = await retriever.GetWheelSettings();

        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result.First().Name, Is.EqualTo("Wheel 1"));
        Assert.That(result.First().Slices.Count(), Is.EqualTo(4));
        Assert.That(result.First().Slices.First().Label, Is.EqualTo("Slice 1"));
        Assert.That(result.First().Slices.First().Size, Is.EqualTo(1));
    }
}