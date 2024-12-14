using Infrastructure.Cryptography;
using Infrastructure.Messaging;
using WebApi.Config;

namespace WebApi;

public static class Startup
{
    public static void SendConfigOptionsToMessageBus(IConfiguration configuration)
    {
        var hashingOptions = new HashingOptions
        {
            HashPrefix = configuration.GetRequiredSection("Hash:HashPrefix").Get<string>() ??
                         throw new ArgumentNullException("Hash prefix must be provided."),
            HashSize = configuration.GetRequiredSection("Hash:HashSize").Get<int>(),
            SaltSize = configuration.GetRequiredSection("Hash:SaltSize").Get<int>()
        };

        MessageBus.Publish(Messages.HashingOptions, hashingOptions);

        var tokenOptions = new TokenifierOptions
        {
            Secret = configuration.GetRequiredSection("Jwt:Key").Get<string>() ??
                     throw new ArgumentNullException("Token Key must be provided.")
        };

        MessageBus.Publish(Messages.TokenifierOptions, tokenOptions);
    }

    public static void SendStorageOptionsToMessageBus(IConfiguration configuration)
    {
        var storage = configuration.GetRequiredSection("Storage").Get<StorageConfig>();

        MessageBus.Publish(Messages.StorageOptions, storage);
    }
}