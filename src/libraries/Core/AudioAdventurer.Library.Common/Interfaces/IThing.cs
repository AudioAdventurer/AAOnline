using AudioAdventurer.Library.Common.Managers;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Events;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThing
    {
        public object Lock { get; }
        public string Name { get; }
        public string FullName { get; set; }
        public string Description { get; }
        public string Title { get; }
        public ThingEventManager EventManager { get; }
        public BehaviorManager BehaviorManager { get; }
        public List<string> Keywords { get; }

        public IThing Parent { get; set; }
        public IReadOnlyCollection<IThing> Children { get; }

        public bool Add(IThing childThing);
        
        public AddChildEvent RequestAdd(IThing thingToAdd);
        
        public RemoveChildEvent RequestRemoval(IThing thingToRemove);

        public bool PerformAdd(
            IThing thingToAdd,
            AddChildEvent addEvent,
            MultipleParentsBehavior multipleParentsBehavior);

        public bool PerformRemoval(
            IThing thingToRemove,
            RemoveChildEvent removalEvent,
            MultipleParentsBehavior multipleParentsBehavior);

        public IThing Combine(IThing thing);
        public bool CanStack(IThing thing);
    }
}
