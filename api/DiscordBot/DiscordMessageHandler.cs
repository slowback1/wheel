using Common.Interfaces;
using Discord;
using Discord.WebSocket;
using DiscordBot.Handlers;
using DiscordBot.Utils;

namespace DiscordBot;

internal class DiscordMessageHandler
{
    public DiscordMessageHandler(IUser self)
    {
        Self = self;
        DataAccess = DataAccessRetriever.GetDataAccess();
    }

    private IUser Self { get; }
    private IDataAccess DataAccess { get; }

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
        await TryHandleOnMessageReceived(message);
    }

    private async Task TryHandleOnMessageReceived(SocketMessage message)
    {
        try
        {
            if (message is not SocketUserMessage userMessage) return;
            var channel = message.Channel as SocketTextChannel;
            if (channel == null) return;
            var content = message.Content;
            if (content is null) return;
            if (!message.MentionedUsers.Select(x => x.Username).Contains("Wheel of Slowback")) return;

            await message.AddReactionAsync(new Emoji("🛞"));

            var context = DiscordMessageParser.ParseMessage(content, message.Author.Id);

            var handler = DiscordHandlerFactory.CreateHandler(context, DataAccess);

            var response = await handler.HandleAsync();

            await message.Channel.SendMessageAsync($"{MentionUtils.MentionUser(message.Author.Id)} {response}");
        }
        catch (Exception e)
        {
            Console.Write(e);
        }
    }
}