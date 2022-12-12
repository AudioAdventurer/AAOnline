using System;
using AudioAdventurer.Library.Common.EventArguments;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.SimpleMessageBus.Objects;

namespace AudioAdventurer.Library.SimpleMessageBus.Managers
{
    public class SimpleMessageBusManager
        : IMessageBus
    {
        public event EventHandler MessageReceived;

        public void SendMessage(IGameEvent gameEvent)
        {
            var busMessage = new BusMessage()
            {
                EventTime = DateTimeOffset.UtcNow,
                Event = gameEvent
            };

            var args = new MessageReceivedEventArgs
            {
                Message = busMessage
            };

            OnMessageReceived(args);
        }

        public void OnMessageReceived(MessageReceivedEventArgs args)
        {
            MessageReceived?.Invoke(this, args);
        }


    }
}
