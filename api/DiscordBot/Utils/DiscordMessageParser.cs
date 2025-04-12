using Common.Data;

namespace DiscordBot.Utils;

public static class DiscordMessageParser
{
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