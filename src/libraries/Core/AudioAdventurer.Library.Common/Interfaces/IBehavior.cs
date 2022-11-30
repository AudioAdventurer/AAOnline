using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehavior
    {
        public IThing Parent { get; }

        public void SetParent(IThing parent);

        public IBehaviorInfo GetBehaviorInfo();
        public void SetBehaviorInfo(IBehaviorInfo info);
    }
}