using System;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Resolvers
{
    public class CoreBehaviorResolver : IBehaviorResolver
    {
        private IThingService _thingService;

        public CoreBehaviorResolver(
            IThingService thingService)
        {
            _thingService = thingService;
        }

        public IBehavior ResolveBehavior(IBehaviorData behaviorInfo)
        {
            if (behaviorInfo.BehaviorType.Equals(nameof(ExitBehavior)))
            {
                return new ExitBehavior(behaviorInfo, _thingService);
            }

            return null;
        }
    }
}
