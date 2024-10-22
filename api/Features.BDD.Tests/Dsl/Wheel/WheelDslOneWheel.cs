using Common.Interfaces;

namespace Features.BDD.Tests.Dsl;

public class WheelDslOneWheel : WheelDsl
{
    public WheelDslOneWheel(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    protected override async Task SetupData()
    {
        await CreateWheel(FirstWheelName);
    }
}