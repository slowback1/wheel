using DiscordBot.Utils;

namespace DiscordBot.Tests.Utils;

[TestFixture]
public class DiscordMessageParserTests
{
    [Test]
    public void ParseWheelSetting_ShouldParseValidMessage()
    {
        var message = "Option1,Option2,Option3";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.IsNotNull(result);
        Assert.That(result.Slices.Count(), Is.EqualTo(3));
        Assert.IsTrue(result.Slices.Any(slice => slice.Label == "Option1"));
        Assert.IsTrue(result.Slices.Any(slice => slice.Label == "Option2"));
        Assert.IsTrue(result.Slices.Any(slice => slice.Label == "Option3"));
    }

    [Test]
    public void ParseWheelSetting_ShouldStripMentions()
    {
        var message = "<@123456789> Option1,Option2";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.IsNotNull(result);
        Assert.That(result.Slices.Count(), Is.EqualTo(2));
        Assert.IsTrue(result.Slices.Any(slice => slice.Label == "Option1"));
        Assert.IsTrue(result.Slices.Any(slice => slice.Label == "Option2"));
    }

    [Test]
    public void ParseWheelSetting_ShouldHandleEmptyMessage()
    {
        var message = "";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.IsNotNull(result);
        Assert.IsEmpty(result.Slices);
    }

    [Test]
    public void StripMentions_ShouldRemoveSingleMention()
    {
        var message = "<@123456789> Hello World";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.That(result.Slices.First().Label, Is.EqualTo("Hello World"));
    }

    [Test]
    public void StripMentions_ShouldRemoveMultipleMentions()
    {
        var message = "<@123456789> Hello <@987654321> World";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.That(result.Slices.First().Label, Is.EqualTo("Hello  World"));
    }

    [Test]
    public void StripMentions_ShouldReturnOriginalMessageIfNoMentions()
    {
        var message = "Hello World";

        var result = DiscordMessageParser.ParseWheelSetting(message);

        Assert.That(result.Slices.First().Label, Is.EqualTo(message));
    }
}