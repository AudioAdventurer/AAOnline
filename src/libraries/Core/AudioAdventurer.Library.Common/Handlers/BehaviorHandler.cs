using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Handlers
{
    public class BehaviorHandler : IBehaviorHandler
    {
        private readonly List<IBehavior> _managedBehaviors;

        public BehaviorHandler(
            IThing parent,
            IEnumerable<IBehavior> behaviors = null)
        {
            Parent = parent;

            if (behaviors != null)
            {
                _managedBehaviors = behaviors.ToList();
            }
            else
            {
                _managedBehaviors = new List<IBehavior>();
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

        public U FindFirst<U>() where U : IBehavior
        {
            lock (_managedBehaviors)
            {
                return _managedBehaviors
                    .OfType<U>()
                    .FirstOrDefault();
            }
        }
    }
}
