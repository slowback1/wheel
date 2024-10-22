using Data.InMemory;
using Features.BDD.Tests.Dsl;
using TechTalk.SpecFlow;

namespace Features.BDD.Tests.StepDefinitions;

[Binding]
public sealed class WheelCreationStepDefinitions
{
    private WheelDsl Feature { get; set; }

    [Before]
    public void ClearData()
    {
        InMemoryWheelStore.Wheels.Clear();
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
        var wheels = Feature.LastLoadedWheels;

        Assert.That(wheels, Is.Not.Null);
        Assert.That(wheels.Count(), Is.EqualTo(2));

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
    public void WhenIEditTheWheel()
    {
        Assert.Fail("Feature not implemented");
    }

    [Then(@"The wheel should be updated")]
    public async Task ThenTheWheelShouldBeUpdated()
    {
        var wheel = await Feature.GetWheel(Feature.FirstWheelName);

        Assert.That(wheel, Is.Not.Null);

        Assert.That(wheel.Slices.Count(), Is.EqualTo(2));
        Assert.That(wheel.Slices.Last().Label, Is.EqualTo("New Slice"));
        Assert.That(wheel.Slices.Last().Size, Is.EqualTo(2));
    }
}