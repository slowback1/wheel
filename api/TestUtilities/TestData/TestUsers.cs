using Common.Data;

namespace TestUtilities.TestData;

public static class TestUsers
{
    public static User GetTestUser()
    {
        return new User
        {
            Username = "TestUser"
        };
    }

    public static CreateUser GetTestCreateUser()
    {
        return new CreateUser
        {
            Username = "TestUser",
            Password = "TestPassword"
        };
    }

    public static LoginUser GetTestLoginUser()
    {
        return new LoginUser
        {
            Username = "TestUser",
            Password = "TestPassword"
        };
    }
}