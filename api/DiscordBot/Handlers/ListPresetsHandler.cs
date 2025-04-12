using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("list-presets")]
public class ListPresetsHandler : BaseDiscordHandler, IDiscordHandler
{
    public ListPresetsHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public Task<string> HandleAsync()
    {
        throw new NotImplementedException();
    }
}