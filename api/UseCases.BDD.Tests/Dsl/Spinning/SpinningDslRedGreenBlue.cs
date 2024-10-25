using Common.Data;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public class SpinningDslArgs : SpinningDsl
{
    private readonly string[] _args;

    public SpinningDslArgs(string[] args)
    {
        _args = args;
    }

    protected override WheelSetting GetWheel()
    {
        return new WheelSetting
        {
            Name = "RedGreenBlue",
            Slices = _args.Select(a => new WheelSlice
            {
                Label = a,
                Size = 1
            })
        };
    }
}