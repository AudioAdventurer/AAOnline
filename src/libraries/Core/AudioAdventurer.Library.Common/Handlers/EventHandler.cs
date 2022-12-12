using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Handlers
{
    /// <summary>
    /// Used by Thing objects to dispatch events
    /// </summary>
    public class EventHandler : IEventHandler
    {
        private IThing _parent;
        private readonly IMessageBus _messageBus;

        public EventHandler(
            IThing parent,
            IMessageBus messageBus)
        {
            _parent = parent;
            _messageBus = messageBus;
        }

        public void SendMessage(IGameEvent gameEvent)
        {
            _messageBus.SendMessage(gameEvent);
        }

        public void SendCommandMessage(string command, IThing actor)
        {
            var sce = new SubmitCommandEvent
            {
                Actor = actor,
                CommandText = command
            };

            SendMessage(sce);
        }
    }
}
