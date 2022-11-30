using AudioAdventurer.Library.Common.Interfaces;
using System;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class FollowingBehavior 
        : AbstractBehavior
    {
        public FollowingBehavior(IBehaviorInfo behaviorInfo)
            : base(behaviorInfo)
        {
        }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetProperties(IBehaviorInfo behaviorInfo)
        {
            throw new NotImplementedException();
        }

        public override IBehaviorInfo GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
