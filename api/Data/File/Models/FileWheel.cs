using System.Collections.Generic;

namespace Data.File.Models;

internal class FileWheel
{
    public string Name { get; set; }
    public string Username { get; set; }
    public List<FileWheelSlice> Slices { get; set; }
}

internal class FileWheelSlice
{
    public int Size { get; set; }
    public string Label { get; set; }
}