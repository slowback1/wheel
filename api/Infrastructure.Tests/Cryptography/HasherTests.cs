using Infrastructure.Cryptography;

namespace Infrastructure.Tests.Cryptography;

[TestFixture]
public class HasherTests
{
    private readonly HashingOptions _options = new HashingOptions
    {
        SaltSize = 16,
        HashSize = 32,
        HashPrefix = "HASH"
    };

    private Hasher _hasher;

    [SetUp]
    public void SetUp()
    {
        _hasher = new Hasher(_options);
    }

    [Test]
    public void Hash_ShouldReturnValidHash()
    {
        var hash = _hasher.Hash("password");
        Assert.That(hash, Does.StartWith($"${_options.HashPrefix}$V1$"));
    }

    [Test]
    public void Verify_ShouldReturnTrueForValidHash()
    {
        var hash = _hasher.Hash("password");
        var result = _hasher.Verify("password", hash);
        Assert.IsTrue(result);
    }

    [Test]
    public void Verify_ShouldReturnFalseForInvalidHash()
    {
        var hash = _hasher.Hash("password");
        var result = _hasher.Verify("wrongpassword", hash);
        Assert.IsFalse(result);
    }

    [Test]
    public void IsHashSupported_ShouldReturnTrueForSupportedHash()
    {
        var hash = _hasher.Hash("password");
        var result = _hasher.IsHashSupported(hash);
        Assert.IsTrue(result);
    }

    [Test]
    public void IsHashSupported_ShouldReturnFalseForUnsupportedHash()
    {
        var result = _hasher.IsHashSupported("unsupportedhash");
        Assert.IsFalse(result);
    }

    [Test]
    public void Constructor_ShouldThrowExceptionForInvalidSaltSize()
    {
        var options = new HashingOptions { SaltSize = 0, HashSize = 32, HashPrefix = "HASH" };
        Assert.Throws<ArgumentException>(() => new Hasher(options));
    }

    [Test]
    public void Constructor_ShouldThrowExceptionForInvalidHashSize()
    {
        var options = new HashingOptions { SaltSize = 16, HashSize = 0, HashPrefix = "HASH" };
        Assert.Throws<ArgumentException>(() => new Hasher(options));
    }

    [Test]
    public void Constructor_ShouldThrowExceptionForInvalidHashPrefix()
    {
        var options = new HashingOptions { SaltSize = 16, HashSize = 32, HashPrefix = "" };
        Assert.Throws<ArgumentException>(() => new Hasher(options));
    }
}