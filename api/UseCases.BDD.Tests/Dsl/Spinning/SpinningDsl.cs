using Common.Data;
using UseCases.Spinning;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public abstract class SpinningDsl
{
    private List<SpinResult> SpinHistory { get; } = new();
    private string? LastErrorMessage { get; set; }
    private WheelSpinOptions? NextSpinOptions { get; set; }

    protected abstract WheelSetting GetWheel();

    public void RigTheWheelToLandOn(string result)
    {
        var index = GetWheel().Slices.ToList().FindIndex(s => s.Label == result);

        NextSpinOptions = new WheelSpinOptions { RiggedSlice = index };
    }

    public void SetTheSpinningMode(string mode)
    {
        Assert.Fail("Not Implemented");
    }

    public void SpinTheWheel(int times = 1)
    {
        var wheel = GetWheel();

        var useCase = new WheelSpinningUseCase();
        var result = useCase.SpinTheWheel(wheel, NextSpinOptions);

        if (result.Status == FeatureResultStatus.Error)
            LastErrorMessage = result.Exception!.Message;

        if (result.Data != null)
            SpinHistory.Add(result.Data);
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

    public void AssertThatWheelSpinErroredWith(string errorMessage)
    {
        Assert.That(LastErrorMessage, Contains.Substring(errorMessage));
    }
}