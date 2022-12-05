using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Managers;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThing : IThingData
    {
        public ThingEventManager EventManager { get; }
        public BehaviorManager BehaviorManager { get; }
        
        public IReadOnlyCollection<Guid> Parents { get;  }
        public IReadOnlyCollection<Guid> Children { get; }

        bool AddChild(IThing childThing);
        bool RemoveChild(IThing childThing);
        
        public bool AddParent(IThing parentToAdd);
        public bool RemoveParent(IThing parentToRemove);

        public IThingData GetThingData();
    }
}
