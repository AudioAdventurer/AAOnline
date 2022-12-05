using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class RemoveChildEvent : CancellableGameEvent
    {
        public RemoveChildEvent(
            IThing activeThing, SensoryMessage sensoryMessage = null) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
