using AudioAdventurer.Library.Data.Objects;
using System.Collections.Generic;
using System;

namespace AudioAdventurer.Library.Data.Interfaces;

public interface IBehaviorInfoRepo : IRepo<BehaviorData>
{
    public IEnumerable<BehaviorData> GetThingBehaviors(Guid thingId);
}