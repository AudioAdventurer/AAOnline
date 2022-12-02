using AudioAdventurer.Library.Common.Interfaces;

using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class LeaveEvent
        : MovementEvent
    {
        public LeaveEvent(
            IThing activeThing,
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
