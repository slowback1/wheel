using System.Collections.Generic;
using Data.File.Models;

namespace Data.File.Store;

internal class FileStore
{
    public IEnumerable<FileUser> Users { get; set; } = new List<FileUser>();
    public IEnumerable<FileWheel> Wheels { get; set; } = new List<FileWheel>();
}