namespace AudioAdventurer.Library.Common.Interfaces;

public interface IBehaviorFactory
{
    public IBehavior ConstructBehavior(
        IBehaviorData behaviorInfo,
        IThingService thingService);   
}