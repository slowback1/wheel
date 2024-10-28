using Common.Data;
using UseCases.Spinning;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public abstract class SpinningDsl
{
    private List<SpinResult> SpinHistory { get; } = new();

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

        var useCase = new WheelSpinningUseCase();
        var result = useCase.SpinTheWheel(wheel);

        SpinHistory.Add(result);
    }

    public void AssertThatWheelLandedOn(string[] expected)
    {
        AssertThatWheelHasLandedOnResultNTimes(expected, 1);
    }

    public void AssertThatWheelHasLandedOnResultNTimes(string result, int times)
    {
        var count = SpinHistory.Count(s => s.GetLandedLabel() == result);

        Assert.That(count, Is.EqualTo(times));
    }

    public void AssertThatWheelHasLandedOnResultNTimes(string[] result, int times)
    {
        var count = SpinHistory.Count(s => result.Contains(s.GetLandedLabel()));

        Assert.That(count, Is.EqualTo(times));
    }

    public void AssertThatWheelLandedOn(string[] expected, int times)
    {
        var lastResults = SpinHistory.OrderByDescending(s => s).Take(times)
            .Select(s => s.GetLandedLabel());

        foreach (var name in lastResults) Assert.That(expected, Has.Member(name));
    }
}