using Newtonsoft.Json;

namespace Data.File.Store;

internal class FileStoreRetriever : IFileStoreRetriever
{
    private readonly FileStorageSettings _settings;

    public FileStoreRetriever(FileStorageSettings settings)
    {
        _settings = settings;
    }

    public FileStore GetFileStore()
    {
        var path = _settings.StoragePath;

        if (!System.IO.File.Exists(path)) return new FileStore();

        var json = System.IO.File.ReadAllText(path);

        var maybeFileStore = JsonConvert.DeserializeObject<FileStore>(json);

        return maybeFileStore ?? new FileStore();
    }

    public void SaveFileStore(FileStore fileStore)
    {
        var path = _settings.StoragePath;

        var json = JsonConvert.SerializeObject(fileStore);

        System.IO.File.WriteAllText(path, json);
    }
}