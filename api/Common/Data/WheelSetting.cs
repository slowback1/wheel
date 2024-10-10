using System.Collections.Generic;

namespace Common.Data;

public class WheelSetting
{
    public string Name { get; set; }
    public IEnumerable<WheelSlice> Slices { get; set; }
}

public class WheelSlice
{
    public string Label { get; set; }
    public int Size { get; set; }
}