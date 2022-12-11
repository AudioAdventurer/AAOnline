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
            _behaviorData = behaviorInfo;
            _lock = new object();
        }

        public Guid Id => _behaviorData.Id;

        public Guid ParentId => _behaviorData.ParentId;

        public void SetParent(IThing parent)
        {
            _behaviorData.ParentId = parent.Id;
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
