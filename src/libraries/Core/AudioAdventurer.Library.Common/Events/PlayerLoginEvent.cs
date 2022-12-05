using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class PlayerLoginEvent 
        : CancellableGameEvent
    {
        public PlayerLoginEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
