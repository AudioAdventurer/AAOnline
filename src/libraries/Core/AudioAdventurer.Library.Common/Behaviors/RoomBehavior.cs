using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class RoomBehavior : AbstractBehavior
    {
        public RoomBehavior(IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
