using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class WorldBehavior 
        : AbstractBehavior
    {
        public WorldBehavior(IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }


        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
