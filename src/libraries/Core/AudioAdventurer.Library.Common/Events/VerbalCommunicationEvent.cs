using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class VerbalCommunicationEvent 
        : CancellableGameEvent
    {
        public VerbalCommunicationEvent(
            IThing activeThing,
            SensoryMessage sensoryMessage,
            VerbalCommunicationType communicationType)
            : base(activeThing, sensoryMessage)
        {
            CommunicationType = communicationType;
        }

        public VerbalCommunicationType CommunicationType { get; }
    }
}
