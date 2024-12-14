using System.Collections.Generic;
using System.Linq;
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

    protected List<FileUser> Users { get; set; }
    protected List<FileWheel> Wheels { get; set; }

    protected void Load()
    {
        var fileStore = _retriever.GetFileStore();
        Users = fileStore.Users.ToList();
        Wheels = fileStore.Wheels.ToList();
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