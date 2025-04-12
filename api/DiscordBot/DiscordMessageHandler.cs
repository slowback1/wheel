using Common.Data;
using Discord;
using Discord.WebSocket;
using UseCases.Spinning;

namespace DiscordBot;

internal class DiscordMessageHandler
{
    public DiscordMessageHandler(IUser self)
    {
        Self = self;
    }

    private IUser Self { get; }

    public async Task OnMessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after,
        ISocketMessageChannel channel)
    {
        Console.WriteLine("Received Message!");
        var message = await before.GetOrDownloadAsync();
        Console.WriteLine($"{message} -> {after}");
    }

    public async Task OnReady()
    {
        Console.WriteLine("Bot is ready!");
    }

    public async Task OnMessageReceived(SocketMessage message)
    {
        if (message is not SocketUserMessage userMessage) return;
        var channel = message.Channel as SocketTextChannel;
        if (channel == null) return;
        var content = message.Content;
        if (content is null) return;
        if (!message.MentionedUsers.Select(x => x.Username).Contains("Wheel of Slowback")) return;

        await message.AddReactionAsync(new Emoji("🛞"));

        var contentWithoutMention = stripMentions(content);

        var wheelOptions = contentWithoutMention.Split(',');

        var spinner = new WheelSpinningUseCase();

        var options = wheelOptions.Select(x => new WheelSlice
        {
            Label = x,
            Size = 1
        });

        var wheel = new WheelSetting
        {
            Slices = options.ToList(),
            Name = "Discord Bot Wheel"
        };

        var result = spinner.SpinTheWheel(wheel, new WheelSpinOptions { Mode = WheelSpinMode.Random });

        if (result.Status == FeatureResultStatus.Ok)
        {
            var resultMessage = $"{MentionUtils.MentionUser(message.Author.Id)} {result.Data!.GetLandedLabel()}";
            await message.Channel.SendMessageAsync(resultMessage);
        }
        else
        {
            await message.Channel.SendMessageAsync("I am sorry, but I cannot spin the wheel right now.");
        }
    }

    private string stripMentions(string message)
    {
        var mentionIndex = message.IndexOf("<@");
        if (mentionIndex == -1) return message;
        var endIndex = message.IndexOf('>', mentionIndex);
        if (endIndex == -1) return message;
        return message.Remove(mentionIndex, endIndex - mentionIndex + 1);
    }
}