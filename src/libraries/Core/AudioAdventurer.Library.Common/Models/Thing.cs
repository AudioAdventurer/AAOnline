using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Managers;

namespace AudioAdventurer.Library.Common.Models
{
    public class Thing : IThing
    {
        public Thing(
            IThingInfo thingInfo,
            IEnumerable<IBehavior> behaviors)
        {
            Lock = new object();
            Info = thingInfo;

            _children = new List<IThing>();
            Keywords = new List<string>();

            EventManager = new ThingEventManager(this);
            
            var behaviorManager = new BehaviorManager(this);
            behaviorManager.Add(behaviors);
            BehaviorManager = behaviorManager;
        }

        private readonly List<IThing> _children;

        public object Lock { get; }

        public IThingInfo Info { get; }

        public IThing Parent { get; set; }
        public IReadOnlyCollection<IThing> Children => _children.AsReadOnly();

        public ThingEventManager EventManager { get; }
        public BehaviorManager BehaviorManager { get; }
        public List<string> Keywords { get; }

        public bool Add(IThing childThing)
        {
            lock (Lock)
            {
                lock (childThing.Lock)
                {
                    // If the thing already has a parent, ensure we have permission to continue.
                    // Presence of MultipleParentsBehavior means we can always add more parents, but
                    // if it's not present, we have to see if removal would be accepted first.
                    var multipleParentsBehavior = childThing.FindBehavior<MultipleParentsBehavior>();
                    RemoveChildEvent removalRequest = null;

                    var oldParent = childThing.Parent;
                    if (oldParent != null
                        && multipleParentsBehavior == null)
                    {
                        removalRequest = oldParent.RequestRemoval(childThing);
                        if (removalRequest.IsCanceled)
                        {
                            return false;
                        }
                    }

                    var addRequest = RequestAdd(childThing);
                    if (addRequest.IsCanceled)
                    {
                        return false;
                    }

                    // If we got this far, both removal (if needed) and add requests were accepted, so 
                    // perform both now and send the confirmation events for any listeners.  Removal is
                    // first, since we don't want to risk accidentally removing from the new parent, etc.
                    if (removalRequest != null)
                    {
                        oldParent.PerformRemoval(
                            childThing, 
                            removalRequest,
                            multipleParentsBehavior);
                    }

                    PerformAdd(
                        childThing, 
                        addRequest, 
                        multipleParentsBehavior);
                }
            }

            return true;
        }

        public AddChildEvent RequestAdd(IThing thingToAdd)
        {
            // Prepare an add event request, and ensure both the new parent (this) and the 
            // thing itself both get a chance to cancel this request before committing.
            var addChildEvent = new AddChildEvent(thingToAdd, this);
            EventManager.OnMovementRequest(addChildEvent, EventScope.SelfDown);
            thingToAdd.EventManager.OnMovementRequest(addChildEvent, EventScope.SelfDown);
            return addChildEvent;
        }

        public RemoveChildEvent RequestRemoval(IThing thingToRemove)
        {
            // Create and raise a removal event request.
            var removeChildEvent = new RemoveChildEvent(thingToRemove);
            EventManager.OnMovementRequest(removeChildEvent, EventScope.SelfDown);
            return removeChildEvent;
        }

        public bool PerformAdd(
            IThing thingToAdd, 
            AddChildEvent addEvent, 
            MultipleParentsBehavior multipleParentsBehavior)
        {
            // If an existing thing is stackable with the added thing, combine the new
            // thing into the existing thing instead of simply adding it.
            foreach (IThing currentThing in Children)
            {
                if (thingToAdd.CanStack(currentThing))
                {
                    currentThing.Combine(thingToAdd);
                    return true;
                }
            }

            // The item cannot be combined to an existing stack, so add the item as a child of the specified parent.
            if (!_children.Contains(thingToAdd))
            {
                _children.Add(thingToAdd);
            }

            // Tell the child who the new parent is too. Via the MultipleParentsBehavior if applicable.
            if (multipleParentsBehavior == null)
            {
                thingToAdd.Parent = this;
            }
            else
            {
                multipleParentsBehavior.AddParent(this);
            }

            EventManager.OnMovementEvent(
                addEvent, 
                EventScope.SelfDown);
            return true;
        }

        public bool PerformRemoval(
            IThing thingToRemove, 
            RemoveChildEvent removalEvent, 
            MultipleParentsBehavior multipleParentsBehavior)
        {
            if (removalEvent.IsCanceled)
            {
                return false;
            }

            // Send the removal event.
            EventManager.OnMovementEvent(
                removalEvent, 
                EventScope.SelfDown);

            // If the thing to remove was in our Children collection, remove it.
            if (_children.Contains(thingToRemove))
            {
                _children.Remove(thingToRemove);
            }

            // If we don't have a MultipleParentsBehavior, directly remove the one-allowed 
            // parent ourselves, else use the behavior's logic for adjusting the parents.
            if (multipleParentsBehavior == null)
            {
                thingToRemove.Parent = null;
            }
            else
            {
                multipleParentsBehavior.RemoveParent(this);
            }

            return true;
        }

        public IThing Combine(IThing thing)
        {
            lock (Lock)
            {
                lock (thing.Lock)
                {
                    if (!CanStack(thing))
                    {
                        // Return the full original (stack of) thing as the unstacked remainder.
                        return thing;
                    }

                    return null;
                }
            }
        }

        public bool CanStack(IThing thing)
        {
            return false;
        }
    }
}
