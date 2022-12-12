using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IMessageBus
{
    public event EventHandler MessageReceived;

    public void SendMessage(IGameEvent gameEvent);
}