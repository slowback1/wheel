namespace Data.File.Store;

internal interface IFileStoreRetriever
{
    FileStore GetFileStore();
    void SaveFileStore(FileStore fileStore);
}