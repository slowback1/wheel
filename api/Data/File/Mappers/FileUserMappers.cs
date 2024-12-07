using Common.Data;
using Data.File.Models;

namespace Data.File.Mappers;

internal static class FileUserMappers
{
    public static FileUser ToFileUser(this CreateUser createUser)
    {
        return new FileUser
        {
            Username = createUser.Username
        };
    }

    public static CreateUser ToCreateUser(this FileUser fileUser)
    {
        return new CreateUser
        {
            Username = fileUser.Username,
            Password = fileUser.PasswordHash
        };
    }

    public static FileUser ToFileUser(this LoginUser loginUser)
    {
        return new FileUser
        {
            PasswordHash = loginUser.Password,
            Username = loginUser.Username
        };
    }

    public static LoginUser ToLoginUser(this FileUser fileUser)
    {
        return new LoginUser
        {
            Username = fileUser.Username,
            Password = fileUser.PasswordHash
        };
    }

    public static User ToUser(this FileUser fileUser)
    {
        return new User
        {
            Username = fileUser.Username
        };
    }

    public static FileUser ToFileUser(this User user)
    {
        return new FileUser
        {
            Username = user.Username
        };
    }
}