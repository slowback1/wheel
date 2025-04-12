namespace DiscordBot.Handlers;

[AttributeUsage(AttributeTargets.Class)]
public class DiscordActionAttribute : Attribute
{
    public DiscordActionAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}