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
!add <name>|<comma separated list> - Adds a new preset with the specified name
!spin-preset <name> - Spins the wheel with the specified preset
!update <name>|<comma separated list> - Updates the specified preset with the new settings
!describe <name> - Lists the possible options for the specified preset
!delete <name> - Deletes the specified preset 
";
    }
}