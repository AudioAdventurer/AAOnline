using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.SimpleMessageBus.Objects
{
    /// <summary>
    /// Wrapper for a Event Message
    /// </summary>
    public class BusMessage : IMessageBusMessage
    {
        public DateTimeOffset EventTime { get; set; }
        public IGameEvent Event { get; set; }
    }
}
