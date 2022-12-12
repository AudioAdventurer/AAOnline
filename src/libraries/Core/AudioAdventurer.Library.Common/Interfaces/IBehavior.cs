using System;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehavior
    {
        public IThing Parent { get; }

        public IBehaviorData GetProperties();

        public void SetParent(IThing parent);
    }
}