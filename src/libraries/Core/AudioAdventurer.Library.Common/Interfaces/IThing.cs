using System.Collections.Generic;
using AudioAdventurer.Library.Common.Managers;
using AudioAdventurer.Library.Common.Events;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThing
    {
        public object Lock { get; }
        public IThingData Info { get; }
        public ThingEventManager EventManager { get; }
        public BehaviorManager BehaviorManager { get; }
        
        public IReadOnlyCollection<IThing> Parents { get;  }
        public IReadOnlyCollection<IThing> Children { get; }

        bool AddChild(IThing childThing);
        bool RemoveChild(IThing childThing);


        public bool PerformChildAdd(
            IThing thingToAdd,
            AddChildEvent addEvent);

        public bool PerformChildRemoval(
            IThing thingToRemove,
            RemoveChildEvent removalEvent);

        public bool PerformParentAdd(
            IThing parentToAdd);

        public bool PerformParentRemoval(
            IThing parentToRemove);

        public IThing Combine(IThing thing);
        public bool CanStack(IThing thing);
    }
}
