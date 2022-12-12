using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IMessageBusMessage
{
    public DateTimeOffset EventTime { get; set; }
    public IGameEvent Event { get; set; }
}