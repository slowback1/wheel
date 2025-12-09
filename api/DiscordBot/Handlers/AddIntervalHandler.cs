using System;
using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("add-interval")]
public class AddIntervalHandler : BaseDiscordHandler, IDiscordHandler
{
    public AddIntervalHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        if (!Context.Argument.Contains('|'))
            return "Invalid format. Please use: !add-interval <intervalName>|<presetName>|<frequency>";

        var parts = Context.Argument.Split('|');
        if (parts.Length != 3)
            return "Invalid format. Please use: !add-interval <intervalName>|<presetName>|<frequency>";

        var intervalName = parts[0].Trim();
        var presetName = parts[1].Trim();
        var frequencyStr = parts[2].Trim().ToLower();

        // Validate preset exists
        var preset = await DataAccess.WheelRetriever.GetWheelSetting(presetName);
        if (preset == null)
            return $"Preset '{presetName}' not found. Please create the preset first.";

        // Parse frequency
        if (!TryParseFrequency(frequencyStr, out var frequency))
            return $"Invalid frequency '{frequencyStr}'. Valid values: hourly, daily, weekly, monthly, quarterly, yearly";

        // Create interval
        var result = await DataAccess.IntervalCreator.CreateInterval(new CreateInterval
        {
            Name = intervalName,
            Username = user.Username,
            PresetName = presetName,
            Frequency = frequency
        });

        if (!result.SaveSuccessful)
            return result.ErrorMessage ?? "Failed to create interval";

        return $"Interval '{intervalName}' created successfully. It will run {frequencyStr} and spin preset '{presetName}'.";
    }

    private bool TryParseFrequency(string frequencyStr, out IntervalFrequency frequency)
    {
        frequency = IntervalFrequency.Hourly;
        
        return frequencyStr switch
        {
            "hourly" => SetFrequency(out frequency, IntervalFrequency.Hourly),
            "daily" => SetFrequency(out frequency, IntervalFrequency.Daily),
            "weekly" => SetFrequency(out frequency, IntervalFrequency.Weekly),
            "monthly" => SetFrequency(out frequency, IntervalFrequency.Monthly),
            "quarterly" => SetFrequency(out frequency, IntervalFrequency.Quarterly),
            "yearly" => SetFrequency(out frequency, IntervalFrequency.Yearly),
            _ => false
        };
    }

    private bool SetFrequency(out IntervalFrequency frequency, IntervalFrequency value)
    {
        frequency = value;
        return true;
    }
}
