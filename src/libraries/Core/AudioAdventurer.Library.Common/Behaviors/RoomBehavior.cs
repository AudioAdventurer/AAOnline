using AudioAdventurer.Library.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class RoomBehavior : AbstractBehavior
    {
        public RoomBehavior(IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }

        public override void SetProperties(
            Dictionary<string, string> behaviorInfo)
        {
            
        }

        public override IBehaviorData GetProperties()
        {
            return this._behaviorData;
        }
    }
}
