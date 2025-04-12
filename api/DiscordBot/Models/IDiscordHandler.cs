namespace DiscordBot.Models;

public interface IDiscordHandler
{
    Task<string> HandleAsync();
}