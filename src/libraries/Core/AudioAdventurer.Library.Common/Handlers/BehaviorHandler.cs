using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Handlers
{
    /// <summary>
    /// Used by Thing objects to handle attached behaviors
    /// </summary>
    public class BehaviorHandler : IBehaviorHandler
    {
        private readonly List<IBehavior> _managedBehaviors;

        public BehaviorHandler(
            IThing parent,
            IEnumerable<IBehavior> behaviors = null)
        {
            Parent = parent;
            _managedBehaviors = new List<IBehavior>();

            if (behaviors != null)
            {
                foreach (var behavior in behaviors)
                {
                    _managedBehaviors.Add(behavior);
                    behavior.SetParent(parent);
                }
            }
        }

        public IThing Parent { get; }

        public IReadOnlyCollection<IBehavior> AllBehaviors
        {
            get
            {
                lock (_managedBehaviors)
                {
                    return _managedBehaviors
                        .AsReadOnly();
                }
            }
        }

        public void Add(IBehavior newBehavior)
        {
            lock (_managedBehaviors)
            {
                if (!_managedBehaviors.Contains(newBehavior))
                {
                    newBehavior.SetParent(Parent);
                    _managedBehaviors.Add(newBehavior);
                }
            }
        }

        public void Add(IEnumerable<IBehavior> newBehaviors)
        {
            if (newBehaviors == null)
            {
                return;
            }

            foreach (var behavior in newBehaviors)
            {
                Add(behavior);
            }
        }

        public void Remove(IBehavior behavior)
        {
            lock (_managedBehaviors)
            {
                if (_managedBehaviors.Contains(behavior))
                {
                    _managedBehaviors.Remove(behavior);
                    behavior.SetParent(null);
                }
            }
        }

        public T FindFirst<T>() 
            where T : IBehavior
        {
            lock (_managedBehaviors)
            {
                return _managedBehaviors
                    .OfType<T>()
                    .FirstOrDefault();
            }
        }

        public IEnumerable<T> Find<T>() 
            where T : IBehavior
        {
            lock (_managedBehaviors)
            {
                return _managedBehaviors
                    .OfType<T>();
            }
        }

        public void SetSession(ISession session)
        {
            foreach (var behavior in _managedBehaviors)
            {
                if (behavior is IRequiresSession rs)
                {
                    rs.Session = session;
                }
            }
        }

        public void RemoveSession()
        {
            
        }
    }
}
