using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Handlers;
using EventHandler = AudioAdventurer.Library.Common.Handlers.EventHandler;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThing : IThingData
    {
        public EventHandler EventHandler { get; }
        public BehaviorHandler BehaviorHandler { get; }

        public IThingService ThingService { get; }

        public object Lock { get; }
        
        public IReadOnlyCollection<Guid> Parents { get;  }
        public IReadOnlyCollection<Guid> Children { get; }

        bool AddChild(IThing childThing);
        bool RemoveChild(IThing childThing);
        
        public bool AddParent(IThing parentToAdd);
        public bool RemoveParent(IThing parentToRemove);

        public IThingData GetThingData();
        public void AddBehavior<T>(T behavior) where T : IBehavior;
    }
}
