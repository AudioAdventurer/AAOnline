using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehavior
    {
        public IThing Parent { get; }

        public void SetParent(IThing parent);

        public void SetProperties(Dictionary<string, string> behaviorInfo);

        public IBehaviorData GetProperties();
    }
}