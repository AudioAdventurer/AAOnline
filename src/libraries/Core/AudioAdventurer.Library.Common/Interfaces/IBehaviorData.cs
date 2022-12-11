using System;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IBehaviorData : IIdentifiableObject
    {
        public Guid ParentId { get; set; }

        public string BehaviorType { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}