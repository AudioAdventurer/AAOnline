using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class AreaBehavior 
        : AbstractBehavior
    {
        public AreaBehavior(IBehaviorInfo behaviorInfo) 
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
