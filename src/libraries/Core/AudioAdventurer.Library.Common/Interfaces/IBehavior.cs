using System;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehavior
    {
        public Guid ParentId { get; }

        public IBehaviorData GetProperties();

        public void SetParent(IThing parent);
    }
}