using System;
using System.Collections.Generic;
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
        
        public override void SetProperties(Dictionary<string, string> behaviorInfo)
        {
            throw new NotImplementedException();
        }

        public override IBehaviorData GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
