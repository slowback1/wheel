using System;

namespace Common.Data;

public class Interval
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string PresetName { get; set; }
    public IntervalFrequency Frequency { get; set; }
    public DateTime LastRunTime { get; set; }
    public DateTime NextRunTime { get; set; }
}

public class CreateInterval
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string PresetName { get; set; }
    public IntervalFrequency Frequency { get; set; }
}

public enum IntervalFrequency
{
    Hourly,
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    Yearly
}
