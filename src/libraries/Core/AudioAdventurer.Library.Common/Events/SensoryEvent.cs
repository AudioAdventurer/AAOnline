using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    internal class SensoryEvent 
        : AbstractGameEvent
    {
        public SensoryEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
