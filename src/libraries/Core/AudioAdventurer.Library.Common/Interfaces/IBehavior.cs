using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehavior
    {
        public IThing Parent { get; }

        public void SetParent(IThing parent);

        public IBehaviorData GetBehaviorInfo();
        public void SetBehaviorInfo(IBehaviorData info);
    }
}