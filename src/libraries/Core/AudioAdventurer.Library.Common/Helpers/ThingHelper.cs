using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ThingHelper
    {
        public static T FindBehavior<T>(this IThing thing) 
            where T : IBehavior
        {
            return thing.BehaviorManager.FindFirst<T>();
        }

        public static IEnumerable<T> FindAllChildrenBehaviors<T>(
            this IThing thing)
            where T : IBehavior
        {
            return from child in thing.Children
                let behavior = child.FindBehavior<T>()
                select behavior;
        }

        public static bool HasBehavior<T>(this IThing thing)
            where T : IBehavior
        {
            return thing.BehaviorManager.FindFirst<T>() != null;
        }

        public static AddChildEvent RequestChildAdd(
            this IThing thing, 
            IThing thingToAdd)
        {
            // Prepare an add event request, and ensure both the new parent (this) and the 
            // thing itself both get a chance to cancel this request before committing.
            var addChildEvent = new AddChildEvent(
                thingToAdd, 
                thing);

            thing.EventManager.OnMovementRequest(
                addChildEvent, 
                EventScope.SelfDown);
            thingToAdd.EventManager.OnMovementRequest(
                addChildEvent, 
                EventScope.SelfDown);

            return addChildEvent;
        }

        public static RemoveChildEvent RequestChildRemoval(
            this IThing thing, 
            IThing thingToRemove)
        {
            // Create and raise a removal event request.
            var removeChildEvent = new RemoveChildEvent(thingToRemove);
            thing.EventManager.OnMovementRequest(
                removeChildEvent, 
                EventScope.SelfDown);
            return removeChildEvent;
        }

        public static void RemoveFromParents(this IThing thing)
        {
            foreach (var parent in thing.Parents)
            {
                parent.RemoveChild(thing);
            }
        }
    }
}
