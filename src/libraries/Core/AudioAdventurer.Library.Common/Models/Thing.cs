using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Handlers;
using AudioAdventurer.Library.Common.Interfaces;
using EventHandler = AudioAdventurer.Library.Common.Handlers.EventHandler;

namespace AudioAdventurer.Library.Common.Models
{
    public class Thing : IThing
    {
        private readonly IThingData _thingData;
        private readonly List<Guid> _children;
        private readonly List<Guid> _parents;
        private readonly IThingService _thingService;
        private readonly object _lock;

        public Thing(
            IThingData data,
            IEnumerable<Guid> parents,
            IEnumerable<Guid> children,
            IThingService thingService)
        {
            _thingData = data;
            _children = children.ToList();
            _parents = parents.ToList();

            _thingService = thingService;
            EventHandler = new EventHandler(
                this, 
                thingService.GetMessageBus());

            BehaviorHandler = new BehaviorHandler(
                this);

            _lock = new object();
        }

        public Thing(
            IThingData data,
            IEnumerable<IBehavior> behaviors,
            IEnumerable<Guid> parents,
            IEnumerable<Guid> children,
            IThingService thingService)
        {
            _thingData = data;
            _children = children.ToList();
            _parents = parents.ToList();

            _thingService = thingService;
            
            EventHandler = new EventHandler(
                this,
                _thingService.GetMessageBus());

            BehaviorHandler = new BehaviorHandler(
                this, 
                behaviors);
            _lock = new object();
        }

        public IReadOnlyCollection<Guid> Parents
        {
            get
            {
                lock (_lock)
                {
                    return _parents.AsReadOnly();
                }
            }
        }

        public IReadOnlyCollection<Guid> Children
        {
            get
            {
                lock (_lock)
                {
                    return _children.AsReadOnly();
                }
            }
        }

        public EventHandler EventHandler { get; }

        public BehaviorHandler BehaviorHandler { get; }

        public IThingService ThingService => _thingService;

        public object Lock => _lock;

        public Guid Id
        {
            get => _thingData.Id;
            set => _thingData.Id = value;
        }

        public string Name
        {
            get => _thingData.Name;
            set => _thingData.Name = value;
        }

        public string Description
        {
            get => _thingData.Description;
            set => _thingData.Description = value;
        }

        public string Title
        {
            get => _thingData.Title;
            set => _thingData.Title = value;
        }

        public string SingularPrefix
        {
            get => _thingData.SingularPrefix;
            set => _thingData.SingularPrefix = value;
        }

        public string PluralSuffix
        {
            get => _thingData.PluralSuffix;
            set => _thingData.PluralSuffix = value;
        }

        public int MaxChildren
        {
            get => _thingData.MaxChildren;
            set => _thingData.MaxChildren = value;
        }

        public int MaxParents
        {
            get => _thingData.MaxParents;
            set => _thingData.MaxParents = value;
        }

        public bool AddChild(IThing childThing)
        {
            lock (_lock)
            {
                lock (childThing)
                {
                    if (!_children.Contains(childThing.Id))
                    {
                        if (_children.Count < MaxChildren)
                        {
                            // If child doesn't have this as a parent
                            if (!childThing.Parents.Contains(this.Id))
                            {
                                // Add this a the parent of the child
                                if (childThing.AddParent(this))
                                {
                                    // Add child id to children collection
                                    _children.Add(childThing.Id);

                                    return true;
                                }
                            }
                            else
                            {
                                // It already does so just add to children
                                _children.Add(childThing.Id);

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool RemoveChild(IThing oldChild)
        {
            // No two threads may add/remove any combination of the parent/sub-thing at the same time,
            // in order to prevent race conditions resulting in thing-disconnection/duplication/etc.
            lock (_lock)
            {
                lock (oldChild)
                {
                    _children.Remove(oldChild.Id);

                    if (oldChild.Parents.Contains(Id))
                    {
                        oldChild.RemoveParent(this);
                    }

                    return true;
                }
            }
        }

        public bool AddParent(IThing newParent)
        {
            lock (_lock)
            {
                if (!_parents.Contains(newParent.Id))
                {
                    // If the number of parents allowed is not yet reached
                    // just add the new parent
                    if (_parents.Count < MaxParents)
                    {
                        _parents.Add(newParent.Id);

                        if (!newParent.Children.Contains(this.Id))
                        {
                            newParent.AddChild(this);
                        }

                        return true;
                    }

                    // If there is already one parent and
                    // max parents is one.  remove this item
                    // from the parent
                    if (MaxParents == 1
                        && _parents.Count == 1)
                    {
                        var oldParentId = _parents.First();

                        var oldParent = _thingService.GetThing(oldParentId);
                        if (oldParent != null)
                        {
                            if (oldParent.Children.Contains(this.Id))
                            {
                                // Remove the parent from this
                                _parents.Remove(oldParentId);

                                // Remove this from the parent
                                if (oldParent.RemoveChild(this))
                                {
                                    if (!_parents.Contains(newParent.Id))
                                    {
                                        _parents.Add(newParent.Id);
                                    }

                                    // Now add this to the new parent
                                    newParent.AddChild(this);

                                    return true;
                                }

                                // Couldn't remove child from parent
                                return false;
                            }

                            // Not there so nothing to do
                            return true;
                        }
                    }

                    // More than one parent is allowed,
                    // and we have reached the limit of parents
                    // so we don't know which parent to replace
                    // or what to do.  So we do nothing
                    return false;
                }

                // Can only add a parent once
                return false;
            }
        }

        public bool RemoveParent(IThing oldParent)
        {
            lock (_lock)
            {
                lock (oldParent)
                {
                    if (_parents.Contains(oldParent.Id))
                    {
                        _parents.Remove(oldParent.Id);
                    }

                    if (oldParent.Children.Contains(this.Id))
                    {
                        oldParent.RemoveChild(this);
                    }

                    return true;
                }
            }
        }

        public IThingData GetThingData()
        {
            lock (_lock)
            {
                return _thingData;
            }
        }

        public void AddBehavior<T>(T behavior)
            where T : IBehavior
        {
            BehaviorHandler.Add(behavior);
        }
    }
}
