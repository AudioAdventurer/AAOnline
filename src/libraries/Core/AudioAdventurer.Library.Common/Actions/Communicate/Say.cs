using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Actions.Communicate
{
    public class Say : IGameAction
    {
        private readonly IServerOutputWriter _writer;

        public Say(
            IServerOutputWriter writer)
        {
            _writer = writer;
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

            // Send event for others
            var sayEvent = new VerbalCommunicationEvent(
                actor, 
                sm, 
                VerbalCommunicationType.Say);
            actor.EventHandler.SendMessage(sayEvent);

            if (actionInput.Session != null)
            {
                var output = _writer.WriteSayOutput(sayText);
                actionInput.Session.WriteServerOutput(output);
            }
        }
    }
}
