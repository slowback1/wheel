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

    [Test]
    public void ParseMessage_ShouldReturnEmptyContext_WhenMessageIsEmpty()
    {
        var message = "";

        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Command, Is.Empty);
        Assert.That(result.Argument, Is.Empty);
    }

    [Test]
    public void ParseMessage_ShouldParseCommandWhenGivenValidSyntax()
    {
        var message = "!command argument";

        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Command, Is.EqualTo("command"));
    }

    [Test]
    public void ParseMessage_StripsMentions()
    {
        var message = "<@123456789> !command argument";

        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Command, Is.EqualTo("command"));
    }

    [Test]
    public void ParseMessage_ReturnsCommand_WhenNoArgument()
    {
        var message = "!command";

        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Command, Is.EqualTo("command"));
    }

    [Test]
    public void ParseMessage_LowercasesCommand()
    {
        var message = "!COMMAND argument";

        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Command, Is.EqualTo("command"));
    }

    [Test]
    [TestCase("!command argument", "argument")]
    [TestCase("<@123456789> !command argument", "argument")]
    [TestCase("!command", "")]
    [TestCase("!command ", "")]
    [TestCase("!command argument that is long", "argument that is long")]
    public void ParseMessage_ShouldReturnCorrectArgument(string message, string expectedArgument)
    {
        var result = DiscordMessageParser.ParseMessage(message);

        Assert.IsNotNull(result);
        Assert.That(result.Argument, Is.EqualTo(expectedArgument));
    }

    [Test]
    public void ParseMessage_PassesTheUserIdThrough()
    {
        var message = "<@123456789> !command argument";
        var userId = 123456789;

        var result = DiscordMessageParser.ParseMessage(message, (ulong)userId);

        Assert.IsNotNull(result);
        Assert.That(result.UserId, Is.EqualTo((ulong)userId));
    }
}