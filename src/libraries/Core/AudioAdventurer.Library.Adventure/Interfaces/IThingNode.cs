using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Adventure.Interfaces;

public interface IThingNode
{
    public IThingData ThingInfo { get; set; }
    public List<IBehaviorData> BehaviorInfos { get; set; }
}