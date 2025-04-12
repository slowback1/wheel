using Discord;
using Discord.WebSocket;
using DiscordBot;
using Microsoft.Extensions.Configuration;

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

await Task.Delay(-1);