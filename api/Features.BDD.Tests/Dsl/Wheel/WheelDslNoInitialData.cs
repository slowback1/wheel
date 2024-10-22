using Common.Interfaces;

namespace Features.BDD.Tests.Dsl;

public class WheelDslNoInitialData : WheelDsl
{
    public WheelDslNoInitialData(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    protected override Task SetupData()
    {
        return Task.CompletedTask;
    }
}