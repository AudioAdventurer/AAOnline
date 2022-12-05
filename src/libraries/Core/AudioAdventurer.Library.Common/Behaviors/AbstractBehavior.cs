using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public abstract class AbstractBehavior : IBehavior
    {
        protected readonly object _lock;
        protected readonly IBehaviorData _behaviorData;

        protected AbstractBehavior(
            IBehaviorData behaviorInfo)
        {
            Id = behaviorInfo.Id;
            _behaviorData = behaviorInfo;
            _lock = new object();
        }

        public Guid Id { get; set; }

        public IThing Parent { get; private set; }

        public void SetParent(IThing newParent)
        {
            if (Parent != newParent)
            {
                if (Parent != null)
                {
                    OnRemoveBehavior();
                }

                Parent = newParent;
                if (newParent != null)
                {
                    OnAddBehavior();
                }
            }
        }

        protected virtual void OnAddBehavior()
        {
        }

        protected virtual void OnRemoveBehavior()
        {
        }

        public abstract IBehaviorData GetProperties();
    }
}
