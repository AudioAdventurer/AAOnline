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
        
        public override void SetProperties(
            Dictionary<string, string> behaviorInfo)
        {
            
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
