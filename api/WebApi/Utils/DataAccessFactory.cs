using Common.Interfaces;
using Data.InMemory;

namespace WebApi.Utils;

public static class DataAccessFactory
{
    public static IDataAccess CreateDataAccess()
    {
        return new InMemoryDataAccess();
    }
}