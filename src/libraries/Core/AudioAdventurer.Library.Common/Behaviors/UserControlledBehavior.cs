using System;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Sessions;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class UserControlledBehavior
        : AbstractBehavior
    {
        public UserControlledBehavior(IBehaviorInfo behaviorInfo)
            : base(behaviorInfo)
        {
        }

        public Session Session { get; set; }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetProperties(IBehaviorInfo behaviorInfo)
        {
            throw new NotImplementedException();
        }

        public override IBehaviorInfo GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}
