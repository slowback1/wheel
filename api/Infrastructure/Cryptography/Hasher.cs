using System;
using System.Security.Cryptography;

namespace Infrastructure.Cryptography;

public struct HashingOptions
{
    public int SaltSize;
    public int HashSize;
    public string HashPrefix;
}

public class Hasher
{
    public Hasher(HashingOptions options)
    {
        Options = options;
        ValidateHashingOptions();
    }

    private HashingOptions Options { get; }

    private void ValidateHashingOptions()
    {
        if (Options.SaltSize <= 0)
            throw new ArgumentException("Salt size must be greater than zero.", nameof(Options.SaltSize));
        if (Options.HashSize <= 0)
            throw new ArgumentException("Hash size must be greater than zero.", nameof(Options.HashSize));
        if (string.IsNullOrWhiteSpace(Options.HashPrefix))
            throw new ArgumentException("Hash prefix must be provided.", nameof(Options.HashPrefix));
    }

    /// <summary>
    ///     Creates a hash from a string.
    /// </summary>
    /// <param name="str">The string to hash.</param>
    /// <param name="iterations">Number of iterations.</param>
    /// <returns>The hash.</returns>
    public string Hash(string str, int iterations = 10000)
    {
        // Create salt
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[Options.SaltSize]);

        // Create hash
        var pbkdf2 = new Rfc2898DeriveBytes(str, salt, iterations);
        var hash = pbkdf2.GetBytes(Options.HashSize);

        // Combine salt and hash
        var hashBytes = new byte[Options.SaltSize + Options.HashSize];
        Array.Copy(salt, 0, hashBytes, 0, Options.SaltSize);
        Array.Copy(hash, 0, hashBytes, Options.SaltSize, Options.HashSize);

        // Convert to base64
        var base64Hash = Convert.ToBase64String(hashBytes);

        return $"${Options.HashPrefix}$V1${iterations}${base64Hash}";
    }

    /// <summary>
    ///     Checks if hash is supported.
    /// </summary>
    /// <param name="hashString">The hash.</param>
    /// <returns>Is supported?</returns>
    public bool IsHashSupported(string hashString)
    {
        return hashString.Contains($"${Options.HashPrefix}$V1$");
    }

    /// <summary>
    ///     Verifies a string against a hash.
    /// </summary>
    /// <param name="inputString">The plaintext string.</param>
    /// <param name="hashedString">The hash.</param>
    /// <returns>Could be verified?</returns>
    public bool Verify(string inputString, string hashedString)
    {
        if (!IsHashSupported(hashedString)) throw new NotSupportedException("The hashtype is not supported");

        var splittedHashString = hashedString.Replace($"${Options.HashPrefix}$V1$", "").Split('$');
        var iterations = int.Parse(splittedHashString[0]);
        var base64Hash = splittedHashString[1];

        var hashBytes = Convert.FromBase64String(base64Hash);

        var salt = new byte[Options.SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, Options.SaltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(inputString, salt, iterations);
        var hash = pbkdf2.GetBytes(Options.HashSize);

        for (var i = 0; i < Options.HashSize; i++)
            if (hashBytes[i + Options.SaltSize] != hash[i])
                return false;
        return true;
    }
}