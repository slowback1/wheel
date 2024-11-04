using Infrastructure.Cryptography;
using Infrastructure.Messaging;

namespace UseCases.User;

internal static class PasswordUtils
{
    public static string HashPassword(string password)
    {
        var options = MessageBus.GetLastMessage<HashingOptions>(Messages.HashingOptions);

        var hasher = new Hasher(options);

        return hasher.Hash(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var options = MessageBus.GetLastMessage<HashingOptions>(Messages.HashingOptions);

        var hasher = new Hasher(options);

        return hasher.Verify(password, hashedPassword);
    }
}