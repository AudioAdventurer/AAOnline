using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Managers;

namespace AudioAdventurer.Library.Common.Models
{
    public class Thing : IThing
    {
        private readonly IThingData _thingData;
        private readonly List<Guid> _children;
        private readonly List<Guid> _parents;
        private readonly IThingService _thingService;

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
            EventManager = new ThingEventManager(this);
            BehaviorManager = new BehaviorManager(
                this,
                behaviors);
        }

        public IReadOnlyCollection<Guid> Parents
        {
            get
            {
                lock (this)
                {
                    return _parents.AsReadOnly();
                }
            }
        }

        public IReadOnlyCollection<Guid> Children
        {
            get
            {
                lock (this)
                {
                    return _children.AsReadOnly();
                }
            }
        }

        public ThingEventManager EventManager { get; }

        public BehaviorManager BehaviorManager { get; }

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
            lock (this)
            {
                lock (childThing)
                {
                    if (!_children.Contains(childThing.Id))
                    {
                        if (_children.Count < MaxChildren)
                        {
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
            lock (this)
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
            lock (this)
            {
                if (!_parents.Contains(newParent.Id))
                {
                    // If the number of parents allowed is not yet reached
                    // just add the new parent
                    if (_parents.Count < MaxParents)
                    {
                        _parents.Add(newParent.Id);
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
                                    // Now add this to the new parent
                                    newParent.AddChild(this);

                                    // Add the parent to this
                                    _parents.Add(newParent.Id);

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
            lock (this)
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
            lock (this)
            {
                return _thingData;
            }
        }
    }
}
