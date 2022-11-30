using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class MultipleParentsBehavior 
        : AbstractBehavior
    {
        public MultipleParentsBehavior(IBehaviorInfo behaviorInfo)
            : base(behaviorInfo)
        {
        }

        protected override void SetDefaultProperties()
        {
        }

        protected override void SetProperties(IBehaviorInfo behaviorInfo)
        {
            throw new System.NotImplementedException();
        }

        public override IBehaviorInfo GetProperties()
        {
            throw new System.NotImplementedException();
        }

        public void AddParent(Thing newParent)
        {
            lock (_lock)
            {
                // Tracking parents for our attached thing only makes sense if we are indeed attached to a thing.
                // (Avoid race conditions against behavior attachment by using a temporary reference to Parent).
                var thing = Parent;
                if (thing != null)
                {
                    
                }
            }
        }

        public void RemoveParent(Thing oldParent)
        {
            lock (_lock)
            {
                // Tracking parents for our attached thing only makes sense if we are indeed attached to a thing.
                // (Avoid race conditions against behavior attachment by using a temporary reference to Parent).
                var thing = Parent;
                if (thing != null)
                {
                    
                }
            }
        }
    }
}
