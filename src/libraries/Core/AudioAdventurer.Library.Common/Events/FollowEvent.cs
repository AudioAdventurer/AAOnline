using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class FollowEvent 
        : CancellableGameEvent
    {
        public FollowEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage)
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
