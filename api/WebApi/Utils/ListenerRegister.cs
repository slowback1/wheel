using UseCases.Listeners;

namespace WebApi.Utils;

public static class ListenerRegister
{
    public static void RegisterListeners()
    {
        var wheelSettingCreationLogger = new WheelSettingCreationLogger();
    }
}