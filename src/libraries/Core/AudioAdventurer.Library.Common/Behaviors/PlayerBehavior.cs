using AudioAdventurer.Library.Common.Interfaces;
using System;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class PlayerBehavior : AbstractBehavior
    {
        public PlayerBehavior(IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetProperties(IBehaviorData behaviorInfo)
        {
            throw new NotImplementedException();
        }

        public override IBehaviorData GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
