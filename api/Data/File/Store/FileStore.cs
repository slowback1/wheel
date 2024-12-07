using System.Collections.Generic;
using Data.File.Models;

namespace Data.File.Store;

internal class FileStore
{
    public IEnumerable<FileUser> Users { get; set; }
    public IEnumerable<FileWheel> Wheels { get; set; }
}