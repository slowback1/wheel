using Common.Data;
using Common.Interfaces;
using DiscordBot.Handlers;
using DiscordBot.Models;
using TestUtilities.MockImplementations;

namespace DiscordBot.Tests.Handlers;

public class AddIntervalHandlerTests
{
    private IDataAccess DataAccess { get; set; }
    private DiscordActionContext Context { get; set; }

    [SetUp]
    public void SetUp()
    {
        DataAccess = new TestDataAccess();
        Context = new DiscordActionContext
        {
            Argument = "testInterval|TestPreset|hourly",
            Command = "add-interval",
            UserId = 1234567890,
            ChannelId = 9876543210
        };

        // Create a test preset first
        DataAccess.WheelCreator.CreateWheelSetting(new CreateWheelSetting
        {
            Username = "1234567890",
            Name = "TestPreset",
            Slices = new[] { new WheelSlice { Label = "1", Size = 1 } }
        }).Wait();
    }

    [Test]
    public async Task ShouldReturnErrorMessageWhenFormatIsInvalid()
    {
        Context.Argument = "invalid";

        var handler = new AddIntervalHandler(DataAccess, Context);

        var result = await handler.HandleAsync();

        Assert.That(result, Does.Contain("Invalid format"));
    }

    [Test]
    public async Task ShouldReturnErrorMessageWhenFrequencyIsInvalid()
    {
        Context.Argument = "testInterval|TestPreset|invalid";

        var handler = new AddIntervalHandler(DataAccess, Context);

        var result = await handler.HandleAsync();

        Assert.That(result, Does.Contain("Invalid frequency"));
    }
}