using Common.Data;
using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

public abstract class BaseDiscordHandler
{
    protected readonly DiscordActionContext Context;
    protected readonly IDataAccess DataAccess;

    protected BaseDiscordHandler(IDataAccess dataAccess, DiscordActionContext context)
    {
        Context = context;
        DataAccess = dataAccess;
    }


    protected async Task<User> GetOrCreateUser(ulong discordId)
    {
        var userId = discordId.ToString();

        await DataAccess.UserCreator.CreateUser(new CreateUser
        {
            Username = userId,
            Password = userId
        });

        var storedUser = await DataAccess.UserRetriever.GetUser(userId);

        if (storedUser == null)
            throw new Exception($"Failed to create or retrieve user with ID {userId}");

        return storedUser;
    }
}