using AudioAdventurer.GameExtensions.SampleAdventure.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.GameExtensions.SampleAdventure.Factories
{
    public class SampleAdventureBehaviorResolver 
        : IBehaviorFactory
    {
        public IBehavior ConstructBehavior(
            IBehaviorData behaviorInfo,
            IThingService thingService)
        {
            var behaviorType = behaviorInfo.BehaviorType;

            if (behaviorType.Equals(nameof(ParrotBehavior)))
            {
                return new ParrotBehavior(behaviorInfo);
            }

            return null;
        }
    }
}
