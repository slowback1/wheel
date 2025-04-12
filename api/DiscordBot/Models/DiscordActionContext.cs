namespace DiscordBot.Models;

public class DiscordActionContext
{
    public string Argument { get; set; }
    public string Command { get; set; }
    public ulong UserId { get; set; }
}