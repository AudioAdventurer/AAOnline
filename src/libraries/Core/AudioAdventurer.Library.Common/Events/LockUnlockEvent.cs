using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class LockUnlockEvent 
        : CancellableGameEvent
    {
        public LockUnlockEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
