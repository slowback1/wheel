using Discord;
using Discord.WebSocket;
using DiscordBot;
using DiscordBot.Utils;
using Microsoft.Extensions.Configuration;
using ScheduledJobs;

Console.WriteLine("initializing bot");
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var builder = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appSettings.json", false, true)
        .AddJsonFile($"appSettings.{environment}.json", true)
    ;
var configuration = builder.Build();
var discordToken = configuration["Discord:Token"];

if (Environment.GetEnvironmentVariable("DISCORD_TOKEN") is string token) discordToken = token;

var config = new DiscordSocketConfig
{
    MessageCacheSize = 100
};

var client = new DiscordSocketClient(config);


await client.LoginAsync(TokenType.Bot, discordToken);
await client.StartAsync();

var self = client.CurrentUser;
var handler = new DiscordMessageHandler(self);

client.MessageUpdated += handler.OnMessageUpdated;
client.MessageReceived += handler.OnMessageReceived;
client.Ready += handler.OnReady;

// Initialize the interval scheduler
var dataAccess = DataAccessRetriever.GetDataAccess();
var scheduler = new IntervalScheduler(dataAccess, async (channelId, message) =>
{
    try
    {
        if (client.GetChannel(channelId) is IMessageChannel channel)
        {
            await channel.SendMessageAsync(message);
            Console.WriteLine($"Sent message to channel {channelId}: {message}");
        }
        else
        {
            Console.WriteLine($"Channel {channelId} not found. Message: {message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending message to channel {channelId}: {ex.Message}");
    }
});
scheduler.Start();
Console.WriteLine("Interval scheduler started");

await Task.Delay(-1);