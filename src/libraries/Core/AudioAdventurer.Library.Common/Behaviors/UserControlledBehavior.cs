using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Sessions;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class UserControlledBehavior
        : AbstractBehavior
    {
        public UserControlledBehavior(IBehaviorData behaviorInfo)
            : base(behaviorInfo)
        {
        }

        public Session Session { get; set; }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
