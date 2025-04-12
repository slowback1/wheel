using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("usage")]
public class UsageHandler : BaseDiscordHandler, IDiscordHandler
{
    public UsageHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public Task<string> HandleAsync()
    {
        throw new NotImplementedException();
    }
}