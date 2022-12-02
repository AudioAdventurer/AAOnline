using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Managers;

namespace AudioAdventurer.Library.Common.Models
{
    public class Thing : IThing
    {
        private readonly Lazy<List<IThing>> _children;
        private readonly Lazy<List<IThing>> _parents;

        public Thing(
            IThingData thingInfo,
            IEnumerable<IBehavior> behaviors,
            Lazy<List<IThing>> parents = null,
            Lazy<List<IThing>> children = null)
        {
            Lock = new object();
            Info = thingInfo;

            if (children == null)
            {
                _children = new Lazy<List<IThing>>(new List<IThing>());
            }
            else
            {
                _children = children;
            }

            if (parents == null)
            {
                _parents = new Lazy<List<IThing>>(new List<IThing>());
            }
            else
            {
                _parents = parents;
            }

            EventManager = new ThingEventManager(this);
            
            var behaviorManager = new BehaviorManager(this);
            behaviorManager.Add(behaviors);
            BehaviorManager = behaviorManager;
        }


        public object Lock { get; }

        public IThingData Info { get; }

        public IReadOnlyCollection<IThing> Parents =>_parents.Value.AsReadOnly();
        public IReadOnlyCollection<IThing> Children => _children.Value.AsReadOnly();

        public ThingEventManager EventManager { get; }
        public BehaviorManager BehaviorManager { get; }

        public bool AddChild(IThing childThing)
        {
            lock (Lock)
            {
                lock (childThing.Lock)
                {
                    // If the thing already has a parent, ensure we have permission to continue.
                    // Presence of MultipleParentsBehavior means we can always add more parents, but
                    // if it's not present, we have to see if removal would be accepted first.
                    RemoveChildEvent removalRequest = null;

                    if (childThing.Info.MaxParents > 1)
                    {
                        foreach (var parent in childThing.Parents)
                        {
                            removalRequest = parent.RequestChildRemoval(childThing);

                            if (removalRequest.IsCanceled)
                            {
                                return false;
                            }
                        }
                    }

                    var addRequest = this.RequestChildAdd(childThing);
                    if (addRequest.IsCanceled)
                    {
                        return false;
                    }

                    // If we got this far, both removal (if needed) and add requests were accepted, so 
                    // perform both now and send the confirmation events for any listeners.  Removal is
                    // first, since we don't want to risk accidentally removing from the new parent, etc.
                    if (removalRequest != null)
                    {
                        foreach (var parent in childThing.Parents)
                        {
                            parent.PerformChildRemoval(
                                childThing,
                                removalRequest);
                        }
                    }

                    PerformChildAdd(
                        childThing, 
                        addRequest);
                }
            }

            return true;
        }

        public bool RemoveChild(IThing thing)
        {
            // No two threads may add/remove any combination of the parent/sub-thing at the same time,
            // in order to prevent race conditions resulting in thing-disconnection/duplication/etc.
            lock (Lock)
            {
                lock (thing.Lock)
                {
                    if (Children.Contains(thing))
                    {
                        var removalRequest = this.RequestChildRemoval(thing);
                        return PerformChildRemoval(thing, removalRequest);
                    }
                }
            }

            return false;
        }

        public bool PerformChildAdd(
            IThing thingToAdd, 
            AddChildEvent addEvent)
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

            // The item cannot be combined to an existing stack,
            // so add the item as a child of the specified parent.
            var children = _children.Value.ToList();
            if (!children.Contains(thingToAdd))
            {
                children.Add(thingToAdd);
            }

            thingToAdd.PerformParentAdd(this);

            EventManager.OnMovementEvent(
                addEvent, 
                EventScope.SelfDown);
            return true;
        }

        public bool PerformChildRemoval(
            IThing thingToRemove, 
            RemoveChildEvent removalEvent)
        {
            if (removalEvent.IsCanceled)
            {
                return false;
            }

            // Send the removal event.
            EventManager.OnMovementEvent(
                removalEvent, 
                EventScope.SelfDown);

            var children = _children.Value;

            // If the thing to remove was in our Children collection, remove it.
            if (children.Contains(thingToRemove))
            {
                children.Remove(thingToRemove);
            }

            thingToRemove.PerformParentRemoval(this);

            return true;
        }

        public bool PerformParentAdd(
            IThing parentToAdd)
        {
            var parents = _parents.Value;

            if (!parents.Contains(parentToAdd))
            {
                parents.Add(parentToAdd);
            }

            return true;
        }


        public bool PerformParentRemoval(
            IThing parentToRemove)
        {
            var parents = _parents.Value;

            if (parents.Contains(parentToRemove))
            {
                parents.Remove(parentToRemove);
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
