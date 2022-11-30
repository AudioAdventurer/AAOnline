using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Adventure.Interfaces;

public interface IThingNode
{
    public IThingInfo ThingInfo { get; set; }
    public List<IBehaviorInfo> BehaviorInfos { get; set; }
}