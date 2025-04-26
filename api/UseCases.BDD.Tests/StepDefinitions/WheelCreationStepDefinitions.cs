using Common.Data;
using Data.InMemory;
using Infrastructure.Messaging;
using TechTalk.SpecFlow;
using UseCases.BDD.Tests.Dsl;

namespace UseCases.BDD.Tests.StepDefinitions;

[Binding]
public sealed class WheelCreationStepDefinitions
{
    private WheelDsl Feature { get; set; }

    [Before]
    public void ClearData()
    {
        InMemoryStore.Wheels.Clear();
    }

    [Given("I intend to create a wheel")]
    public void GivenIIntendToCreateAWheel()
    {
        var dataAccess = new InMemoryDataAccess();

        Feature = new WheelDslNoInitialData(dataAccess);
    }

    [When("I create a wheel")]
    public async Task CreateAWheel()
    {
        await Feature.CreateWheel(Feature.FirstWheelName);
    }

    [Then("The wheel should be created")]
    public async Task ThenTheWheelShouldBeCreated()
    {
        await Feature.AssertThereIsAnyWheelData();
    }

    [Then("I should be able to access the wheel")]
    public async Task ThenIShouldBeAbleToAccessTheWheel()
    {
        await Feature.AssertWheelExists(Feature.FirstWheelName);
    }

    [Given(@"I have created two wheels")]
    public async Task GivenIHaveCreatedTwoWheels()
    {
        var dataAccess = new InMemoryDataAccess();

        Feature = new WheelDslTwoWheels(dataAccess);
    }

    [When(@"I view my wheels")]
    public async Task WhenIViewMyWheels()
    {
        await Feature.LoadWheels();
    }

    [Then(@"I should see two wheels")]
    public void ThenIShouldSeeTwoWheels()
    {
        var wheels = Feature.LastLoadedWheels!.Data;

        Assert.That(wheels, Is.Not.Null);
        Assert.That(wheels!.Count(), Is.EqualTo(2));

        Assert.That(wheels.Any(w => w.Name == Feature.FirstWheelName), Is.True);
        Assert.That(wheels.Any(w => w.Name == Feature.SecondWheelName), Is.True);
    }

    [Given(@"I have created a wheel")]
    public async Task GivenIHaveCreatedAWheel()
    {
        var dataAccess = new InMemoryDataAccess();

        Feature = new WheelDslOneWheel(dataAccess);
    }

    [When(@"I edit the wheel")]
    public async Task WhenIEditTheWheel()
    {
        var wheel = (await Feature.GetWheel(Feature.FirstWheelName)).Data;

        wheel.Name = "New Name";
        var slices = wheel.Slices.ToList();

        slices.Add(new WheelSlice
        {
            Label = "New Slice",
            Size = 2
        });

        wheel.Slices = slices;

        await Feature.UpdateWheel(wheel);
    }

    [Then(@"The wheel should be updated")]
    public async Task ThenTheWheelShouldBeUpdated()
    {
        var wheel = (await Feature.GetWheel("New Name")).Data;

        Assert.That(wheel, Is.Not.Null);

        Assert.That(wheel.Slices.Count(), Is.EqualTo(2));
        Assert.That(wheel.Slices.Last().Label, Is.EqualTo("New Slice"));
        Assert.That(wheel.Slices.Last().Size, Is.EqualTo(2));
    }

    [When(@"I try to access a wheel that does not exist")]
    public async Task WhenITryToAccessAWheelThatDoesNotExist()
    {
        await Feature.LoadWheel(Feature.NonExistantWheelName);
    }

    [Then(@"I should be notified that the wheel does not exist")]
    public async Task ThenIShouldBeNotifiedThatTheWheelDoesNotExist()
    {
        var wheel = Feature.LastLoadedWheel;

        Assert.That(wheel.Status, Is.EqualTo(FeatureResultStatus.NotFound));
    }

    [When(@"I try to create a wheel with the same name")]
    public async Task WhenITryToCreateAWheelWithTheSameName()
    {
        await Feature.CreateWheel(Feature.FirstWheelName);
    }

    [Then(@"I should be notified that the wheel already exists")]
    public void ThenIShouldBeNotifiedThatTheWheelAlreadyExists()
    {
        var lastCreatedResult = Feature.LastCreatedWheel!;

        Assert.That(lastCreatedResult.Status, Is.EqualTo(FeatureResultStatus.Error));
    }

    [When(@"I try to get my wheels and an error occurs")]
    public async Task WhenITryToGetMyWheelsAndAnErrorOccurs()
    {
        Feature.CauseGetWheelsToError();

        await Feature.LoadWheels();
    }

    [Then(@"I should be notified that an error occurred")]
    public void ThenIShouldBeNotifiedThatAnErrorOccurred()
    {
        Assert.That(Feature.LastLoadedWheels!.Status, Is.EqualTo(FeatureResultStatus.Error));
    }

    [Then(@"The message bus should broadcast the wheel creation")]
    public void ThenTheMessageBusShouldBroadcastTheWheelCreation()
    {
        var lastMessage = MessageBus.GetLastMessage<WheelSetting>(Messages.WheelSettingCreated);

        Assert.That(lastMessage, Is.Not.Null);
        Assert.That(lastMessage!.Name, Is.EqualTo(Feature.FirstWheelName));
    }

    [Given(@"I have an account and have created a wheel")]
    public void GivenIHaveAnAccountAndHaveCreatedAWheel()
    {
        Feature = new WheelDslOneWheel(new InMemoryDataAccess());
    }

    [Given(@"another user has created a wheel")]
    public async Task GivenAnotherUserHasCreatedAWheel()
    {
        await Feature.CreateWheel("Other person's wheel", Feature.SecondLoggedInUserHash);
    }

    [Then(@"I should only see the wheel I created")]
    public async Task ThenIShouldOnlySeeTheWheelICreated()
    {
        await Feature.LoadWheels();
        Feature.AssertWheelDoesNotExist("Other person's wheel");
    }

    [Given(@"I am not logged in and I am trying to access stored wheels")]
    public void GivenIAmNotLoggedInAndIAmTryingToAccessStoredWheels()
    {
        Feature = new WheelNotLoggedInDsl(new InMemoryDataAccess());
    }

    [Then(@"I should be notified that I need to log in to create a wheel")]
    public void ThenIShouldBeNotifiedThatINeedToLogInToCreateAWheel()
    {
        Feature.AssertCreationErrorMessageIs("log in");
    }

    [Then(@"I should be notified that I need to log in to view my wheels")]
    public void ThenIShouldBeNotifiedThatINeedToLogInToViewMyWheels()
    {
        Feature.AssertViewErrorMessageIs("log in");
    }

    [When(@"I try to edit a wheel that does not exist")]
    public async Task WhenITryToEditAWheelThatDoesNotExist()
    {
        await Feature.UpdateWheel(new WheelSetting());
    }

    [When(@"I delete the wheel")]
    public async Task WhenIDeleteTheWheel()
    {
        var wheel = (await Feature.GetWheel(Feature.FirstWheelName)).Data;

        await Feature.DeleteWheel(wheel!.Name);
    }

    [Then(@"The wheel should be deleted")]
    public void ThenTheWheelShouldBeDeleted()
    {
        var lastDeleteResult = Feature.LastDeletedWheelResult!;

        Assert.That(lastDeleteResult.Status, Is.EqualTo(FeatureResultStatus.Ok));
        Assert.That(lastDeleteResult.Data, Is.True);
    }
}