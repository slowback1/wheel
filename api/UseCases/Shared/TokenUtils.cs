using System.Linq;
using Infrastructure.Cryptography;
using Infrastructure.Messaging;

namespace UseCases.Shared;

internal static class TokenUtils
{
    public static string? GetUserFromToken(string token)
    {
        var options = MessageBus.GetLastMessage<TokenifierOptions>(Messages.TokenifierOptions);

        var tokenifier = new Tokenifier(options);

        var claims = tokenifier.GetClaims(token);

        return claims?.FirstOrDefault(c => c.Key == "id").Value;
    }
}