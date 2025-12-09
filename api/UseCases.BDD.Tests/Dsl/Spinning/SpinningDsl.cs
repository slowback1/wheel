using Common.Data;
using UseCases.Spinning;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public abstract class SpinningDsl
{
    private List<SpinResult> SpinHistory { get; } = new();
    private string? LastErrorMessage { get; set; }
    private WheelSpinOptions? NextSpinOptions { get; set; }
    private string? CurrentUserId { get; set; }
    private WheelSpinningUseCase UseCase { get; } = new();

    protected abstract WheelSetting GetWheel();

    public void SetUserId(string userId)
    {
        CurrentUserId = userId;
    }

    public void RigTheWheelToLandOn(string result)
    {
        var index = GetWheel().Slices.ToList().FindIndex(s => s.Label == result);

        NextSpinOptions = new WheelSpinOptions { RiggedSlice = index, Mode = WheelSpinMode.Rigged };
    }

    public void SetTheSpinningMode(string mode)
    {
        NextSpinOptions = new WheelSpinOptions { Mode = GetMode(mode) };
    }

    private WheelSpinMode GetMode(string mode)
    {
        return mode switch
        {
            "random" => WheelSpinMode.Random,
            "rigged" => WheelSpinMode.Rigged,
            "distribution" => WheelSpinMode.Distribution,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }

    public void SpinTheWheel(int times = 1)
    {
        for (var i = 0; i < times; i++) Spin();
    }

    private void Spin()
    {
        var wheel = GetWheel();

        var result = UseCase.SpinTheWheel(wheel, NextSpinOptions, CurrentUserId);

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
        var lastResults = SpinHistory.OrderByDescending(s => s.GetLandedLabel()).Take(times)
            .Select(s => s.GetLandedLabel());

        foreach (var name in lastResults) Assert.That(expected, Has.Member(name));
    }

    public void AssertThatWheelSpinErroredWith(string errorMessage)
    {
        Assert.That(LastErrorMessage, Contains.Substring(errorMessage));
    }

    public void AssertLastTwoSpinsAreDifferent()
    {
        if (SpinHistory.Count < 2)
            Assert.Fail("Not enough spins to compare");

        var lastSpin = SpinHistory[SpinHistory.Count - 1].SliceLanded;
        var previousSpin = SpinHistory[SpinHistory.Count - 2].SliceLanded;

        Assert.That(lastSpin, Is.Not.EqualTo(previousSpin));
    }

    public void AssertLastSpinIs(string expected)
    {
        if (SpinHistory.Count == 0)
            Assert.Fail("No spins recorded");

        var lastSpin = SpinHistory[SpinHistory.Count - 1].GetLandedLabel();
        Assert.That(lastSpin, Is.EqualTo(expected));
    }
}