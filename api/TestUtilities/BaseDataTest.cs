using Common.Interfaces;
using TestUtilities.MockImplementations;

namespace TestUtilities;

public class BaseDataTest
{
    public IDataAccess Data { get; set; }

    [SetUp]
    public void SetUpDataAccess()
    {
        Data = new TestDataAccess();
    }
}