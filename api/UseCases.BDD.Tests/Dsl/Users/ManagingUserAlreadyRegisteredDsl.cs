namespace UseCases.BDD.Tests.Dsl.Users;

public class ManagingUserAlreadyRegisteredDsl : ManagingUsersDsl
{
    private ManagingUserAlreadyRegisteredDsl()
    {
    }

    public static async Task<ManagingUserAlreadyRegisteredDsl> Create()
    {
        var dsl = new ManagingUserAlreadyRegisteredDsl();

        await dsl.Register();

        return dsl;
    }
}