using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Data.File.Models;

namespace Data.File.Mappers;

internal static class FileIntervalMappers
{
    public static Interval ToInterval(this FileInterval fileInterval)
    {
        return new Interval
        {
            Name = fileInterval.Name,
            Username = fileInterval.Username,
            PresetName = fileInterval.PresetName,
            Frequency = Enum.Parse<IntervalFrequency>(fileInterval.Frequency),
            LastRunTime = fileInterval.LastRunTime,
            NextRunTime = fileInterval.NextRunTime
        };
    }

    public static FileInterval ToFileInterval(this CreateInterval createInterval)
    {
        var now = DateTime.UtcNow;
        var nextRun = CalculateNextRunTime(now, createInterval.Frequency);

        return new FileInterval
        {
            Name = createInterval.Name,
            Username = createInterval.Username,
            PresetName = createInterval.PresetName,
            Frequency = createInterval.Frequency.ToString(),
            LastRunTime = DateTime.MinValue,
            NextRunTime = nextRun
        };
    }

    public static IEnumerable<Interval> ToIntervals(this IEnumerable<FileInterval> fileIntervals)
    {
        return fileIntervals.Select(ToInterval);
    }

    private static DateTime CalculateNextRunTime(DateTime from, IntervalFrequency frequency)
    {
        return frequency switch
        {
            IntervalFrequency.Hourly => from.AddHours(1),
            IntervalFrequency.Daily => from.AddDays(1),
            IntervalFrequency.Weekly => from.AddDays(7),
            IntervalFrequency.Monthly => from.AddMonths(1),
            IntervalFrequency.Quarterly => from.AddMonths(3),
            IntervalFrequency.Yearly => from.AddYears(1),
            _ => from.AddHours(1)
        };
    }
}
