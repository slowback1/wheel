namespace Data.File.Store;

public class FileStorageSettings
{
    public FileStorageSettings(string storagePath)
    {
        StoragePath = storagePath;
    }

    public string StoragePath { get; set; }
}