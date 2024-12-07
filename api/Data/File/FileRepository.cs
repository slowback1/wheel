using System.Collections.Generic;
using Data.File.Models;
using Data.File.Store;

namespace Data.File;

internal abstract class FileRepository
{
    private readonly IFileStoreRetriever _retriever;

    protected FileRepository(IFileStoreRetriever retriever)
    {
        _retriever = retriever;
        Load();
    }

    protected IEnumerable<FileUser> Users { get; set; }
    protected IEnumerable<FileWheel> Wheels { get; set; }

    protected void Load()
    {
        var fileStore = _retriever.GetFileStore();
        Users = fileStore.Users;
        Wheels = fileStore.Wheels;
    }

    protected void SaveChanges()
    {
        _retriever.SaveFileStore(new FileStore
        {
            Users = Users,
            Wheels = Wheels
        });
    }
}