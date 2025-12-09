using System;

namespace Data.File.Models;

internal class FileInterval
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string PresetName { get; set; }
    public string Frequency { get; set; }
    public DateTime LastRunTime { get; set; }
    public DateTime NextRunTime { get; set; }
}
