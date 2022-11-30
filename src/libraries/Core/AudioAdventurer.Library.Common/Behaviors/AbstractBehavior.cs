using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public abstract class AbstractBehavior : IBehavior
    {
        protected readonly object _lock = new object();

        protected AbstractBehavior(
            IBehaviorData behaviorInfo)
        {
            Id = Guid.NewGuid();
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

        public IBehaviorData GetBehaviorInfo()
        {
            throw new NotImplementedException();
        }

        public void SetBehaviorInfo(IBehaviorData info)
        {
            throw new NotImplementedException();
        }

        
        protected virtual void OnAddBehavior()
        {
        }

        protected virtual void OnRemoveBehavior()
        {
        }

        protected abstract void SetDefaultProperties();
        protected abstract void SetProperties(IBehaviorData behaviorInfo);
        public abstract IBehaviorData GetProperties();
    }
}
