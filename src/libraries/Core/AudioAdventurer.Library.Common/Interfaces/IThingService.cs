using System;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IThingService
{
    public IThing GetThing(Guid id);

    public IEnumerable<IThing> GetChildren(Guid parentId);
}