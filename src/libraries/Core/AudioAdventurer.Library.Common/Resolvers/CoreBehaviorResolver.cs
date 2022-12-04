using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Resolvers
{
    public class CoreBehaviorResolver : IBehaviorResolver
    {
        private readonly IThingService _thingService;

        public CoreBehaviorResolver(
            IThingService thingService)
        {
            _thingService = thingService;
        }

        public IBehavior ResolveBehavior(IBehaviorData behaviorInfo)
        {
            var behaviorType = behaviorInfo.BehaviorType;

            if (behaviorType.Equals(nameof(AreaBehavior)))
            {
                return new AreaBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(ExitBehavior)))
            {
                return new ExitBehavior(behaviorInfo, _thingService);
            }

            if (behaviorType.Equals(nameof(MovableBehavior)))
            {
                return new MovableBehavior(behaviorInfo, _thingService);
            }

            if (behaviorType.Equals(nameof(PlayerBehavior)))
            {
                return new PlayerBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(RoomBehavior)))
            {
                return new RoomBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(UserControlledBehavior)))
            {
                return new UserControlledBehavior(behaviorInfo);
            }

            if (behaviorType.Equals(nameof(WorldBehavior)))
            {
                return new WorldBehavior(behaviorInfo);
            }
            
            return null;
        }
    }
}
