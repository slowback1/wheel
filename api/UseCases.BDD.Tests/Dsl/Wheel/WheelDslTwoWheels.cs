using Common.Interfaces;

namespace UseCases.BDD.Tests.Dsl;

public class WheelDslTwoWheels : WheelDsl
{
    public WheelDslTwoWheels(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    protected override async Task SetupData()
    {
        await CreateWheel(FirstWheelName);
        await CreateWheel(SecondWheelName);
    }
}