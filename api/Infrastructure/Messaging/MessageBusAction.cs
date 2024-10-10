using System;
using System.Threading.Tasks;

namespace Infrastructure.Messaging
{
    internal class MessageBusAction
    {
        public string Id { get; set; }
        public Func<object, Task> Action { get; set; }
    }
}