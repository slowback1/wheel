using Common.Data;
using DiscordBot.Handlers;
using DiscordBot.Models;
using TestUtilities.MockImplementations;

namespace DiscordBot.Tests.Handlers;

public class ListPresetsHandlerTests
{
    private TestDataAccess DataAccess { get; set; }
    private DiscordActionContext Context { get; set; }

    [SetUp]
    public async Task SetUp()
    {
        DataAccess = new TestDataAccess();

        Context = new DiscordActionContext
        {
            Argument = "",
            Command = "list-presets",
            UserId = 12345
        };
    }

    [Test]
    public async Task ListPresetsHandler_ShouldReturnListOfPresets()
    {
        var handler = new ListPresetsHandler(DataAccess, Context);
        var result = await handler.HandleAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<string>());
        Assert.That(result, Does.Contain("Name"));
    }

    [Test]
    public async Task ListPresetsHandler_ShouldReturnMessageWhenNoPresets()
    {
        var retriever = DataAccess.WheelRetriever as TestWheelRetriever;
        retriever!.WheelSettingsToReturn = new List<WheelSetting>();

        var handler = new ListPresetsHandler(DataAccess, Context);
        var result = await handler.HandleAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<string>());
        Assert.That(result, Does.Contain("No presets found"));
    }
}