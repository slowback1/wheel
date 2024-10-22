using Common.Data;
using Data.InMemory;
using TechTalk.SpecFlow;

namespace Features.BDD.Tests.StepDefinitions;

[Binding]
public sealed class WheelCreationStepDefinitions
{
    private const string WheelName = "Test Wheel";
    private WheelFeatures _wheelFeatures { get; set; }

    private IEnumerable<WheelSetting> Wheels { get; set; }

    [Before]
    public void ClearData()
    {
        InMemoryWheelStore.Wheels.Clear();
    }

    [Given("I intend to create a wheel")]
    public void GivenIIntendToCreateAWheel()
    {
        SetUpWheelFeatures();
    }

    private void SetUpWheelFeatures()
    {
        var dataAccess = new InMemoryDataAccess();

        _wheelFeatures = new WheelFeatures(dataAccess.WheelRetriever, dataAccess.WheelCreator);
    }

    private async Task CreateWheel(string name)
    {
        await _wheelFeatures.CreateWheelSetting(new WheelSetting
        {
            Name = name,
            Slices = new[] { new WheelSlice { Label = "label", Size = 1 } }
        });
    }

    [When("I create a wheel")]
    public async Task CreateAWheel()
    {
        await CreateWheel(WheelName);
    }

    [Then("The wheel should be created")]
    public async Task ThenTheWheelShouldBeCreated()
    {
        var settings = await _wheelFeatures.GetWheelSettings();

        Assert.That(settings.Data, Is.Not.Null);
        Assert.That(settings.Data.Count(), Is.GreaterThan(0));
    }

    [Then("I should be able to access the wheel")]
    public async Task ThenIShouldBeAbleToAccessTheWheel()
    {
        var wheel = await _wheelFeatures.GetWheelSetting(WheelName);

        Assert.That(wheel.Data, Is.Not.Null);

        Assert.That(wheel.Data.Name, Is.EqualTo(WheelName));
    }

    [Given(@"I have created two wheels")]
    public async Task GivenIHaveCreatedTwoWheels()
    {
        SetUpWheelFeatures();

        await CreateWheel("Wheel 1");
        await CreateWheel("Wheel 2");
    }

    [When(@"I view my wheels")]
    public async Task WhenIViewMyWheels()
    {
        Wheels = (await _wheelFeatures.GetWheelSettings()).Data!;
    }

    [Then(@"I should see two wheels")]
    public void ThenIShouldSeeTwoWheels()
    {
        Assert.That(Wheels.Count(), Is.EqualTo(2));

        Assert.That(Wheels.Any(w => w.Name == "Wheel 1"), Is.True);
        Assert.That(Wheels.Any(w => w.Name == "Wheel 2"), Is.True);
    }
}