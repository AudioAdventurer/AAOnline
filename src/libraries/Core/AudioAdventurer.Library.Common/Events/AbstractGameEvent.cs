using System;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public abstract class AbstractGameEvent : IGameEvent, 
        IIdentifiableObject
    {
        protected AbstractGameEvent(
            IThing activeThing, 
            SensoryMessage sensoryMessage)
        {
            Id = Guid.NewGuid();
            ActiveThing = activeThing;
            
            if (sensoryMessage != null)
            {
                SensoryMessage = sensoryMessage;

                // TODO: This if-condition was added to deal with some cases where
                // two ActiveThings are attempted for one action, e.g. "get".
                // Should multiple ActiveThings be supported instead, or maybe
                // there's a way to prevent this scenario?
                if (!SensoryMessage.Context.ContainsKey("ActiveThing"))
                {
                    SensoryMessage.Context.Add("ActiveThing", ActiveThing);
                }

                SensoryMessage.Context.Add(GetType().Name, this);
            }
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public IThing ActiveThing { get; }
        public SensoryMessage SensoryMessage { get; }
    }
}
