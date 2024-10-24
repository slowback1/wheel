using System;
using System.Threading.Tasks;
using Common.Data;
using Infrastructure.Messaging;

namespace UseCases.Listeners;

public class WheelSettingCreationLogger : MessageBusListener<WheelSetting>
{
    public WheelSettingCreationLogger() : base(Messages.WheelSettingCreated)
    {
    }

    public override async Task OnMessage(WheelSetting message)
    {
        Console.WriteLine($"Wheel setting created: {message.Name}");
    }
}