using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class MovementEvent 
        : CancellableGameEvent
    {
        public MovementEvent(
            IThing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }

        public IThing GoingFrom { get; set; }
        public IThing GoingTo { get; set; }
        public IThing GoingVia { get; set; }
    }
}
