using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class UserSensoryBehavior
        : AbstractBehavior,
            IRequiresSession,
            IResponsiveBehavior
    {
        public UserSensoryBehavior(
            IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }

        public ISession Session { get; set; }

        public void RespondAsRequired(IGameEvent gameEvent)
        {
            var parent = Parent;

            if (gameEvent is VerbalCommunicationEvent verbalEvent)
            {
                if (parent.IsSameRoomDifferentThing(verbalEvent))
                {
                    HandleSensoryMessage(verbalEvent);
                }
            }
        }

        private void HandleSensoryMessage(VerbalCommunicationEvent verbalEvent)
        {
            var session = Session;
            var sensoryMessage = verbalEvent.SensoryMessage;

            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Name,
                $"{verbalEvent.ActiveThing.Name} says ",
                false);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                $"\"{sensoryMessage.Message.RawMessage}\"",
                true);

            session.WriteServerOutput(serverOutput);
        }
    }
}
