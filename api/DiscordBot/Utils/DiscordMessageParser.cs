using Common.Data;
using DiscordBot.Models;

namespace DiscordBot.Utils;

public static class DiscordMessageParser
{
    public static DiscordActionContext ParseMessage(string message, ulong userId = 0)
    {
        var stripped = StripMentions(message);

        return new DiscordActionContext
        {
            Argument = GetArgument(stripped),
            Command = GetCommand(stripped),
            UserId = userId
        };
    }

    private static string GetCommand(string message)
    {
        var trimmed = message.Trim();

        var commandIndex = trimmed.IndexOf("!");
        if (commandIndex == -1) return string.Empty;
        var endIndex = trimmed.IndexOf(' ', commandIndex);
        if (endIndex == -1) endIndex = trimmed.Length - 1;

        return trimmed
            .Substring(commandIndex + 1, endIndex - commandIndex)
            .Trim()
            .ToLower();
    }

    private static string GetArgument(string message)
    {
        var commandIndex = message.IndexOf("!");
        if (commandIndex == -1) return string.Empty;
        var endIndex = message.IndexOf(' ', commandIndex);
        if (endIndex == -1) endIndex = message.Length;

        return message.Substring(endIndex).Trim();
    }

    public static WheelSetting ParseWheelSetting(string message)
    {
        var contentWithoutMention = StripMentions(message);
        var wheelOptions = contentWithoutMention.Split(',');

        if (wheelOptions.Length == 0 || (wheelOptions.Length == 1 && string.IsNullOrWhiteSpace(wheelOptions[0])))
            return new WheelSetting
            {
                Slices = new List<WheelSlice>(),
                Name = "Discord Bot Wheel"
            };

        var options = wheelOptions.Select(x => new WheelSlice
        {
            Label = x,
            Size = 1
        });
        return new WheelSetting
        {
            Slices = options.ToList(),
            Name = "Discord Bot Wheel"
        };
    }


    private static string StripMentions(string message)
    {
        var mentionIndex = message.IndexOf("<@");
        if (mentionIndex == -1) return message;
        var endIndex = message.IndexOf('>', mentionIndex);
        if (endIndex == -1) return message;

        return StripMentions(message
            .Remove(mentionIndex, endIndex - mentionIndex + 1)
            .Trim());
    }
}