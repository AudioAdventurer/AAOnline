using AudioAdventurer.Library.Common.Interfaces;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class PlayerBehavior : AbstractBehavior
    {
        public PlayerBehavior(IBehaviorData behaviorInfo)
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
