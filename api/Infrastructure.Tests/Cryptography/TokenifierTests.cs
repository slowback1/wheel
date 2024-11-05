using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Infrastructure.Cryptography;
using Microsoft.IdentityModel.Tokens;

[TestFixture]
public class TokenifierTests
{
    [SetUp]
    public void SetUp()
    {
        _tokenifier = new Tokenifier(_options);
    }

    private readonly TokenifierOptions _options = new()
    {
        Secret = "supersecretkey123490!!!:D1111111"
    };

    private Tokenifier _tokenifier;

    [Test]
    public void GenerateToken_ShouldReturnValidToken()
    {
        var token = _tokenifier.GenerateToken("user123");
        Assert.IsNotNull(token);
    }

    [Test]
    public void GenerateToken_ShouldContainCorrectUserId()
    {
        var token = _tokenifier.GenerateToken("user123");
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
        var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

        Assert.That(userId, Is.EqualTo("user123"));
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("tooshort")]
    public void Constructor_ShouldThrowExceptionForInvalidSecret(string secret)
    {
        var options = new TokenifierOptions { Secret = secret };
        Assert.Throws<ArgumentException>(() => new Tokenifier(options));
    }

    [Test]
    public void GetClaims_ShouldReturnClaimsForValidToken()
    {
        var token = _tokenifier.GenerateToken("user123");
        var claims = _tokenifier.GetClaims(token);

        Assert.IsNotNull(claims);
        Assert.IsTrue(claims.Any(c => c.Key == "id" && c.Value == "user123"));
    }

    [Test]
    public void GetClaims_ShouldThrowExceptionForInvalidToken()
    {
        var invalidToken = "invalid.token.value";
        Assert.Throws<ArgumentException>(() => _tokenifier.GetClaims(invalidToken));
    }

    [Test]
    public void GetClaims_ShouldReturnEmptyForTokenWithNoClaims()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var claims = _tokenifier.GetClaims(tokenString);

        Assert.That(claims, Is.Not.Null);
        var idClaim = claims.FirstOrDefault(c => c.Key == "id");

        Assert.That(idClaim.Value, Is.Null);
    }
}