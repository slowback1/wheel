using Common.Interfaces;

namespace UseCases.BDD.Tests.Dsl;

public class WheelNotLoggedInDsl : WheelDsl
{
    public WheelNotLoggedInDsl(IDataAccess dataAccess) : base(dataAccess)
    {
    }

    protected override async Task SetupData()
    {
        FirstLoggedInUserToken = string.Empty;
        SecondLoggedInUserHash = string.Empty;
    }
}