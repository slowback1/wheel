using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

[DiscordAction("remove-interval")]
public class RemoveIntervalHandler : BaseDiscordHandler, IDiscordHandler
{
    public RemoveIntervalHandler(IDataAccess dataAccess, DiscordActionContext context) : base(dataAccess, context)
    {
    }

    public async Task<string> HandleAsync()
    {
        var user = await GetOrCreateUser(Context.UserId);

        if (string.IsNullOrWhiteSpace(Context.Argument))
            return "Please provide an interval name.";

        var intervalName = Context.Argument.Trim();
        var result = await DataAccess.IntervalDeleter.DeleteInterval(user.Username, intervalName);

        if (!result.SaveSuccessful)
            return result.ErrorMessage ?? "Failed to remove interval";

        return $"Interval '{intervalName}' removed successfully.";
    }
}
