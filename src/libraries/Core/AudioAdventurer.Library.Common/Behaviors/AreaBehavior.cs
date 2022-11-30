using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class AreaBehavior 
        : AbstractBehavior
    {
        public AreaBehavior(IBehaviorData behaviorInfo) 
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
