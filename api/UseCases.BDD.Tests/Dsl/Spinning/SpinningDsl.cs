using Common.Data;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public abstract class SpinningDsl
{
    private List<WheelSlice> SpinHistory { get; } = new();
    protected abstract WheelSetting GetWheel();

    public void RigTheWheelToLandOn(string result)
    {
        Assert.Fail("Not Implemented");
    }

    public void SetTheSpinningMode(string mode)
    {
        Assert.Fail("Not Implemented");
    }

    public void SpinTheWheel(int times = 1)
    {
        var wheel = GetWheel();


        Assert.Fail($"Not Implemented, wheel is: {wheel.Name}");
    }

    public void AssertThatWheelLandedOn(string[] expected)
    {
        AssertThatWheelHasLandedOnResultNTimes(expected, 1);
    }

    public void AssertThatWheelHasLandedOnResultNTimes(string result, int times)
    {
        var count = SpinHistory.Count(s => s.Label == result);

        Assert.That(count, Is.EqualTo(times));
    }

    public void AssertThatWheelHasLandedOnResultNTimes(string[] result, int times)
    {
        var count = SpinHistory.Count(s => result.Contains(s.Label));

        Assert.That(count, Is.EqualTo(times));
    }

    public void AssertThatWheelLandedOn(string[] expected, int times)
    {
        var lastResults = SpinHistory.OrderByDescending(s => s).Take(times)
            .Select(s => s.Label);

        foreach (var name in lastResults) Assert.That(expected, Has.Member(name));
    }
}