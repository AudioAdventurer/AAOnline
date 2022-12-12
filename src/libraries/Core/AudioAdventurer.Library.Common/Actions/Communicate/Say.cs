using AudioAdventurer.Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Actions.Communicate
{
    public class Say : IGameAction
    {
        private readonly IMessageBus _messageBus;
        private readonly IServerOutputWriter _writer;

        public Say(
            IServerOutputWriter writer,
            IMessageBus messageBus)
        {
            _writer = writer;
            _messageBus = messageBus;
        }

        public string Command => "say";
        public string CommandAlias => "'";
        public CommandCategory Category => CommandCategory.Communicate;
        public string Description => "Say something to those nearby.";

        public void Execute(
            IActionInput actionInput, 
            IActionHandler actionHandler)
        {
            var actor = actionInput.Actor;
            var actorRoom = actor.FindParentRoom();

            var sayText = actionInput.Tail;

            var contextMessage = new ContextualString(
                actor,
                actorRoom,
                sayText)
            {
                ToOriginator = $"You say: {sayText}",
                ToReceiver = $"{actor.Name} says: {sayText}",
                ToOthers = $"{actor.Name} says: {sayText}"
            };
            var sm = new SensoryMessage(
                SensoryType.Hearing, 
                100, 
                contextMessage);

            var sayEvent = new VerbalCommunicationEvent(
                actor, 
                sm, 
                VerbalCommunicationType.Say);
            actor.EventHandler.SendMessage(sayEvent);

            var output = _writer.WriteSayOutput(sayText);
            actionInput.Session.WriteServerOutput(output);
        }
    }
}
