using AudioAdventurer.Library.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class WorldBehavior 
        : AbstractBehavior
    {
        public WorldBehavior(IBehaviorData behaviorInfo)
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
