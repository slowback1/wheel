using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApi.Config;

public enum StorageType
{
    InMemory = 0,
    File = 1
}

public class StorageConfig
{
    [JsonConverter(typeof(StringEnumConverter))]
    public StorageType StorageType { get; set; }

    public string? FilePath { get; set; }
}