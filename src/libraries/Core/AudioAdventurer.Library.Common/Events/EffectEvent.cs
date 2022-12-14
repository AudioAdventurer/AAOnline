using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class EffectEvent 
        : CancellableGameEvent
    {
        public EffectEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
