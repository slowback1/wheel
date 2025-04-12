using System.Reflection;
using Common.Interfaces;
using DiscordBot.Models;

namespace DiscordBot.Handlers;

public static class DiscordHandlerFactory
{
    public static IDiscordHandler CreateHandler(DiscordActionContext context, IDataAccess dataAccess)
    {
        var typesWithAttribute = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<DiscordActionAttribute>() != null)
            .ToList();

        foreach (var type in typesWithAttribute)
            if (type.GetCustomAttribute<DiscordActionAttribute>()!.Name == context.Command)
            {
                var constructor = GetConstructor(type);
                if (constructor != null)
                {
                    var parameters = new object[] { dataAccess, context };
                    return (IDiscordHandler)constructor.Invoke(parameters);
                }
            }

        return new UsageHandler(dataAccess, context);
    }

    private static ConstructorInfo? GetConstructor(Type type)
    {
        var constructor = type.GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length == 2 &&
                                 c.GetParameters()[0].ParameterType == typeof(IDataAccess) &&
                                 c.GetParameters()[1].ParameterType == typeof(DiscordActionContext));

        return constructor;
    }
}