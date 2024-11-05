using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Cryptography;

public struct TokenifierOptions
{
    public string Secret { get; set; }
}

public class Tokenifier
{
    private readonly TokenifierOptions _options;

    public Tokenifier(TokenifierOptions options)
    {
        _options = options;

        ValidateOptions();
    }

    private void ValidateOptions()
    {
        if (string.IsNullOrWhiteSpace(_options.Secret)) throw new ArgumentException("Secret must be provided");

        var minBitLength = 256;
        if (Encoding.ASCII.GetBytes(_options.Secret).Length * 8 < minBitLength)
            throw new ArgumentException($"Secret must be at least {minBitLength} bits long");
    }

    public string GenerateToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public IEnumerable<KeyValuePair<string, string>>? GetClaims(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);
        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken.Claims.Select(claim => new KeyValuePair<string, string>(claim.Type, claim.Value));
    }
}