using System.Collections.Generic;
using AudioAdventurer.Library.Adventure.Interfaces;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Adventure.Objects
{
    public class ThingNode : IThingNode
    {
        public ThingNode()
        {
            BehaviorInfos = new List<IBehaviorInfo>();
        }

        public IThingInfo ThingInfo { get; set; }
        public List<IBehaviorInfo> BehaviorInfos { get; set; }
    }
}
