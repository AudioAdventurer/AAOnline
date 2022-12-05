using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class ArriveEvent 
        : MovementEvent
    {
        public ArriveEvent(
            IThing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
