using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class PlayerBehavior : AbstractBehavior
    {
        private IThingService _thingService;

        public PlayerBehavior(
            IBehaviorData behaviorInfo,
            IThingService thingService)
            : base(behaviorInfo)
        {
            _thingService = thingService;
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
