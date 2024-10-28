using TechTalk.SpecFlow;
using UseCases.BDD.Tests.Dsl.Spinning;

namespace UseCases.BDD.Tests.StepDefinitions;

[Binding]
public class SpinningWheelsStepDefinitions
{
    private SpinningDsl Feature { get; set; }

    [Given(@"I have a wheel with ""(.*)"", ""(.*)"", ""(.*)""")]
    public void GivenIHaveAWheelWith(string first, string second, string third)
    {
        Feature = new SpinningDslArgs(new[] { first, second, third });
    }

    [When(@"I spin the wheel")]
    public void WhenISpinTheWheel()
    {
        Feature.SpinTheWheel();
    }

    [Then(@"The wheel should land on one of the slices")]
    public void ThenTheWheelShouldLandOnOrOr()
    {
        Feature.AssertThatWheelLandedOn(new[] { "Red", "Green", "Blue" });
    }

    [Then(@"The wheel should land on ""(.*)""")]
    public void ThenTheWheelShouldLandOn(string expectedResult)
    {
        Feature.AssertThatWheelLandedOn(new[] { expectedResult });
    }

    [When(@"I spin the wheel ""(.*)"" times")]
    public void WhenISpinTheWheelTimes(int numberOfTimes)
    {
        Feature.SpinTheWheel(numberOfTimes);
    }

    [Then(@"The wheel should land on ""(.*)"" or ""(.*)"" or ""(.*)"" ""(.*)"" times")]
    public void ThenTheWheelShouldLandOnOrOrTimes(string first, string second, string third, int times)
    {
        Feature.AssertThatWheelLandedOn(new[] { first, second, third }, times);
    }

    [Given(@"I have rigged the wheel to land on ""(.*)""")]
    public void GivenIHaveRiggedTheWheelToLandOn(string result)
    {
        Feature.RigTheWheelToLandOn(result);
    }

    [When(@"I spin the wheel (.*) times in ""(.*)"" mode")]
    public void WhenISpinTheWheelTimesInMode(int times, string mode)
    {
        Feature.SetTheSpinningMode(mode);
        Feature.SpinTheWheel(times);
    }

    [Then(@"The wheel should land on ""(.*)"" or ""(.*)"" or ""(.*)"" ""(.*)"" times each")]
    public void ThenTheWheelShouldLandOnOrOrTimesEach(string first, string second, string third, int times)
    {
        Feature.AssertThatWheelHasLandedOnResultNTimes(first, times);
        Feature.AssertThatWheelHasLandedOnResultNTimes(second, times);
        Feature.AssertThatWheelHasLandedOnResultNTimes(third, times);
    }

    [Given(@"I have a wheel with ""(.*)"", ""(.*)"", ""(.*)"" with ""(.*)"" having a (.*)% chance")]
    public void GivenIHaveAWheelWithWithHavingAChance(string first, string second, string third,
        string argToIncreasePercentChance, int percentChance)
    {
        var options = new[] { first, second, third };

        Feature = new SpinningDsl50PercentChanceRed(options, argToIncreasePercentChance);
    }

    [Then(@"The wheel should land on ""(.*)"" (.*) times")]
    public void ThenTheWheelShouldLandOnTimes(string expectedResult, int numberOfTimes)
    {
        Feature.AssertThatWheelHasLandedOnResultNTimes(expectedResult, numberOfTimes);
    }


    [Then(@"The wheel should land on '(.*)' or '(.*)' (.*) times")]
    public void ThenTheWheelShouldLandOnOrTimes(string first, string second, int numberOfTimes)
    {
        Feature.AssertThatWheelHasLandedOnResultNTimes(new[] { first, second }, numberOfTimes);
    }

    [Given(@"I have a wheel with no slices")]
    public void GivenIHaveAWheelWithNoSlices()
    {
        Feature = new SpinningDslNoSlices();
    }

    [Then(@"I should see an error message denoting that the wheel has no slices")]
    public void ThenIShouldSeeAnErrorMessageDenotingThatTheWheelHasNoSlices()
    {
        Feature.AssertThatWheelSpinErroredWith("Wheel has no slices");
    }
}