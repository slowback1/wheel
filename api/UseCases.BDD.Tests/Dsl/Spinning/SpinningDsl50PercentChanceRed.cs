using Common.Data;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public class SpinningDsl50PercentChanceRed : SpinningDsl
{
    private readonly IEnumerable<WheelSlice> _slices;

    public SpinningDsl50PercentChanceRed(string[] args, string argToIncreaseChance)
    {
        _slices = args.Select(a => new WheelSlice
        {
            Label = a,
            Size = GetSliceSize(args.Length, a, argToIncreaseChance)
        });
    }

    private int GetSliceSize(int totalSliceCount, string arg, string argToIncreaseChance)
    {
        var shouldIncreaseChance = arg == argToIncreaseChance;
        // This makes the "bigger" slice a 50% chance
        var doubleRemainingSlices = (totalSliceCount - 1) * 2;

        return shouldIncreaseChance ? doubleRemainingSlices : 1;
    }

    protected override WheelSetting GetWheel()
    {
        return new WheelSetting
        {
            Name = "50PercentChanceRed",
            Slices = _slices
        };
    }
}