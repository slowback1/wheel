using Data.File.Models;
using Data.File.Store;

namespace Data.Tests.File.Store;

internal class TestFileStoreRetriever : IFileStoreRetriever
{
    public IEnumerable<FileUser> UsersToGet { get; set; } = new List<FileUser>();
    public IEnumerable<FileWheel> WheelsToGet { get; set; } = new List<FileWheel>();

    public FileStore GetFileStore()
    {
        return new FileStore
        {
            Users = UsersToGet,
            Wheels = WheelsToGet
        };
    }

    public void SaveFileStore(FileStore fileStore)
    {
        UsersToGet = fileStore.Users;
        WheelsToGet = fileStore.Wheels;
    }
}