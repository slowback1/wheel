using DiscordBot.Handlers;
using DiscordBot.Models;
using TestUtilities.MockImplementations;

namespace DiscordBot.Tests.Handlers;

public class DiscordHandlerFactoryTests
{
    [TestCase("list-presets", typeof(ListPresetsHandler))]
    [TestCase("usage", typeof(UsageHandler))]
    [TestCase("spin", typeof(SpinHandler))]
    [TestCase("add", typeof(AddPresetHandler))]
    [TestCase("spin-preset", typeof(SpinPresetHandler))]
    public void CreateHandler_ShouldReturnTheCorrectHandler(string command, Type expectedType)
    {
        var context = new DiscordActionContext
        {
            Argument = "",
            Command = command
        };

        var dataAccess = new TestDataAccess();

        var handler = DiscordHandlerFactory.CreateHandler(context, dataAccess);

        Assert.That(handler, Is.TypeOf(expectedType));
    }

    [Test]
    public void CreateHandler_ReturnsTheUsageHandler_WhenCommandIsUnknown()
    {
        var context = new DiscordActionContext
        {
            Argument = "",
            Command = "unknown-command"
        };

        var dataAccess = new TestDataAccess();

        var handler = DiscordHandlerFactory.CreateHandler(context, dataAccess);

        Assert.That(handler, Is.TypeOf<UsageHandler>());
    }
}