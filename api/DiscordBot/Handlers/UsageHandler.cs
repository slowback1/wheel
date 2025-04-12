using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("usage")]
public class UsageHandler : BaseDiscordHandler, IDiscordHandler
{
    public UsageHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        return @"Usage:
!list-presets - Lists your presets
!spin <comma-separated list> - Spins the wheel with the specified settings
!usage - Displays this message
";
    }
}