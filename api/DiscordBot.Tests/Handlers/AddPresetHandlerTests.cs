using Common.Interfaces;
using DiscordBot.Handlers;
using DiscordBot.Models;
using TestUtilities.MockImplementations;

namespace DiscordBot.Tests.Handlers;

public class AddPresetHandlerTests
{
    private IDataAccess DataAccess { get; set; }
    private DiscordActionContext Context { get; set; }

    [SetUp]
    public void SetUp()
    {
        DataAccess = new TestDataAccess();
        Context = new DiscordActionContext
        {
            Argument = "TestPreset",
            Command = "add",
            UserId = 1234567890
        };
    }

    [Test]
    public async Task ShouldReturnSuccessMessageWhenPresetIsAdded()
    {
        Context.Argument = "TestPreset|1,2,3";

        var handler = new AddPresetHandler(DataAccess, Context);

        var result = await handler.HandleAsync();

        Assert.That(result, Is.EqualTo("Preset 'TestPreset' added successfully."));
    }

    [Test]
    public async Task ShouldReturnErrorMessageWhenPresetNameIsEmpty()
    {
        Context.Argument = "1,2,3";

        var handler = new AddPresetHandler(DataAccess, Context);

        var result = await handler.HandleAsync();

        Assert.That(result,
            Is.EqualTo(
                "Preset name cannot be empty.  Please provide the argument in this format: <presetName>|<option1,option2,...>"));
    }

    [Test]
    public async Task ShouldSaveThroughTheWheelCreator()
    {
        var handler = new AddPresetHandler(DataAccess, Context);

        var result = await handler.HandleAsync();

        var lastCreatedWheelSetting = TestWheelCreator.LastCreatedWheelSetting;

        Assert.That(lastCreatedWheelSetting, Is.Not.Null);
        Assert.That(lastCreatedWheelSetting.Name, Is.EqualTo("TestPreset"));
    }
}