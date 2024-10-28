using Common.Data;

namespace UseCases.BDD.Tests.Dsl.Spinning;

public class SpinningDslNoSlices : SpinningDsl
{
    protected override WheelSetting GetWheel()
    {
        return new WheelSetting
        {
            Name = "No slices",
            Slices = new List<WheelSlice>()
        };
    }
}