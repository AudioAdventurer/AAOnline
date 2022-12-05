using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IThingService
{
    public IThing GetThing(Guid id);
    public void SaveThing(IThing thing);
}