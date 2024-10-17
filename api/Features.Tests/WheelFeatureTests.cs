using Common.Data;
using Infrastructure.Messaging;
using TestUtilities;
using TestUtilities.MockImplementations;
using TestUtilities.TestData;

namespace Features.Tests;

public class WheelFeatureTests : BaseDataTest
{
    [TearDown]
    public void ClearMessageBus()
    {
        MessageBus.ClearMessages();
        MessageBus.ClearSubscribers();
    }

    [Test]
    public async Task CanGetAWheelSettingById()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var result = await features.GetWheelSetting("1");

        Assert.NotNull(result);

        Assert.That(result.Data.Name, Is.EqualTo("Name"));
        Assert.That(result.Data.Slices.Count(), Is.EqualTo(1));
        Assert.That(result.Data.Slices.First().Label, Is.EqualTo("Label"));
        Assert.That(result.Data.Slices.First().Size, Is.EqualTo(1));

        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.Ok));
    }

    [Test]
    public async Task CanGetAWheelSettingByIdNotFound()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var result = await features.GetWheelSetting(TestWheelRetriever.NotFoundId);

        Assert.Null(result.Data);
        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.NotFound));
    }

    [Test]
    public async Task CanGetAWheelSettingByIdError()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var result = await features.GetWheelSetting(TestWheelRetriever.ErrorId);

        Assert.Null(result.Data);
        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.Error));
        Assert.NotNull(result.Exception);
        Assert.That(result.Exception.Message, Is.EqualTo("Error"));
    }

    [Test]
    public async Task CanGetAllWheelSettings()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var result = await features.GetWheelSettings();

        Assert.NotNull(result);
        Assert.That(result.Data.Count(), Is.EqualTo(1));
        Assert.That(result.Data.First().Name, Is.EqualTo("Name"));
        Assert.That(result.Data.First().Slices.Count(), Is.EqualTo(1));
        Assert.That(result.Data.First().Slices.First().Label, Is.EqualTo("Label"));
        Assert.That(result.Data.First().Slices.First().Size, Is.EqualTo(1));
    }

    [Test]
    public async Task CanGetAllWheelSettingsError()
    {
        var retriever = new TestWheelRetriever();
        retriever.SetShouldThrowWhenGettingAllSettings(true);
        var features = new WheelFeatures(retriever, Data.WheelCreator);

        var result = await features.GetWheelSettings();

        Assert.Null(result.Data);
        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.Error));
        Assert.NotNull(result.Exception);
        Assert.That(result.Exception.Message, Is.EqualTo("Error"));
    }


    [Test]
    public async Task ReturnsTheWheelSettingWhenCreatingAWheelSetting()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var settingToCreate = TestWheelSettings.GetTestWheelSetting();

        var result = await features.CreateWheelSetting(settingToCreate);
        Assert.That(result.Data.Name, Is.EqualTo("Name"));
        Assert.That(result.Data.Slices.Count(), Is.EqualTo(1));
        Assert.That(result.Data.Slices.First().Label, Is.EqualTo("Label"));
        Assert.That(result.Data.Slices.First().Size, Is.EqualTo(1));
    }

    [Test]
    public async Task CreatesAWheelSetting()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);
        var settingToCreate = TestWheelSettings.GetTestWheelSetting();
        var result = await features.CreateWheelSetting(settingToCreate);
        Assert.That(TestWheelCreator.LastCreatedWheelSetting, Is.EqualTo(settingToCreate));
    }

    [Test]
    public async Task ReturnsAnErrorResultWhenTheSaveFails()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);
        var result = await features.CreateWheelSetting(new WheelSetting { Name = TestWheelCreator.NameThatFails });
        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.Error));
        Assert.That(result.Exception.Message, Is.EqualTo("Failed to create wheel"));
    }

    [Test]
    public async Task ReturnsAnErrorResultWhenTheSaveErrors()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);
        var result = await features.CreateWheelSetting(new WheelSetting { Name = TestWheelCreator.NameThatErrors });
        Assert.That(result.Status, Is.EqualTo(FeatureResultStatus.Error));
        Assert.That(result.Exception.Message, Is.EqualTo("Error"));
    }

    [Test]
    public async Task SendsAWheelCreatedMessageWhenTheSaveSucceeds()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);

        var settingToCreate = TestWheelSettings.GetTestWheelSetting();

        var result = await features.CreateWheelSetting(settingToCreate);

        var lastMessage = MessageBus.GetLastMessage<WheelSetting>(Messages.WheelSettingCreated);

        Assert.That(lastMessage, Is.Not.Null);
        Assert.That(lastMessage.Name, Is.EqualTo(settingToCreate.Name));
    }

    [Test]
    public async Task DoesNotSendAWheelCreatedMessageWhenTheSaveFails()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);
        var result = await features.CreateWheelSetting(new WheelSetting { Name = TestWheelCreator.NameThatFails });
        Assert.That(MessageBus.GetLastMessage<WheelSetting>(Messages.WheelSettingCreated), Is.Null);
    }

    [Test]
    public async Task DoesNotSendAWheelCreatedMessageWhenTheSaveErrors()
    {
        var features = new WheelFeatures(Data.WheelRetriever, Data.WheelCreator);
        var result = await features.CreateWheelSetting(new WheelSetting { Name = TestWheelCreator.NameThatErrors });
        Assert.That(MessageBus.GetLastMessage<WheelSetting>(Messages.WheelSettingCreated), Is.Null);
    }
}