using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IThingService
{
    public IThing GetThing(Guid id);
    public void SaveThing(IThing thing);

    public IBehaviorData GetEmptyBehaviorData();
    public IThingData GetEmptyThingData();
    public IBehavior FindBehavior(IBehaviorData behaviorInfo);
    public IMessageBus GetMessageBus();
}