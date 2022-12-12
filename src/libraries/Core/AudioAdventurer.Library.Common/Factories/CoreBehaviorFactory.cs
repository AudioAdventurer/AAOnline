using System.Linq;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Factories
{
    public class CoreBehaviorFactory : IBehaviorFactory
    {
        public IBehavior ConstructBehavior(
            IBehaviorData behaviorInfo,
            IThingService thingService)
        {
            var behaviorType = behaviorInfo.BehaviorType;

            if (behaviorType.Equals(nameof(AreaBehavior)))
            {
                return new AreaBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(ExitBehavior)))
            {
                return new ExitBehavior(behaviorInfo, thingService);
            }

            if (behaviorType.Equals(nameof(MovableBehavior)))
            {
                return new MovableBehavior(behaviorInfo, thingService);
            }

            if (behaviorType.Equals(nameof(ObservantBehavior)))
            {
                return new ObservantBehavior(behaviorInfo, thingService);
            }

            if (behaviorType.Equals(nameof(PlayerBehavior)))
            {
                return new PlayerBehavior(behaviorInfo, thingService);
            }

            if (behaviorType.Equals(nameof(RoomBehavior)))
            {
                return new RoomBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(UserControlledBehavior)))
            {
                return new UserControlledBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(UserSensoryBehavior)))
            {
                return new UserSensoryBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(WorldBehavior)))
            {
                return new WorldBehavior(behaviorInfo);
            }
            
            return null;
        }
    }
}
