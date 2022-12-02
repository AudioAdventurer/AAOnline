using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public abstract class AbstractBehavior : IBehavior
    {
        protected readonly object _lock = new object();
        protected IBehaviorData _behaviorData;

        protected AbstractBehavior(
            IBehaviorData behaviorInfo)
        {
            Id = behaviorInfo.Id;
            SetProperties(behaviorInfo.Properties);
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

        public abstract void SetProperties(Dictionary<string, string> behaviorInfo);
        
        public abstract IBehaviorData GetProperties();
    }
}
