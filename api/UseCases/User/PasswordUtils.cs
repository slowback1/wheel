using Infrastructure.Cryptography;
using Infrastructure.Messaging;

namespace UseCases.User;

internal static class UserUtils
{
    public static string HashPassword(string password)
    {
        var hasher = GetHasher();

        return hasher.Hash(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var hasher = GetHasher();

        return hasher.Verify(password, hashedPassword);
    }

    public static string GenerateJWT(string username)
    {
        var tokenifier = GetTokenifier();

        return tokenifier.GenerateToken(username);
    }

    private static Tokenifier GetTokenifier()
    {
        var options = MessageBus.GetLastMessage<TokenifierOptions>(Messages.TokenifierOptions);

        var tokenifier = new Tokenifier(options);

        return tokenifier;
    }

    private static Hasher GetHasher()
    {
        var options = MessageBus.GetLastMessage<HashingOptions>(Messages.HashingOptions);

        var hasher = new Hasher(options);

        return hasher;
    }
}