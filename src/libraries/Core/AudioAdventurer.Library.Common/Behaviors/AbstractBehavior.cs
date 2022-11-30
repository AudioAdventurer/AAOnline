using System;
using System.Text.Json.Serialization;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public abstract class AbstractBehavior
    {
        protected readonly object _lock = new object();

        protected AbstractBehavior(
            IBehaviorInfo behaviorInfo)
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [JsonIgnore]
        public Thing Parent { get; private set; }

        public void SetParent(Thing newParent)
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

        protected abstract void SetDefaultProperties();
    }
}
