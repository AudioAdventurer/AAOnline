using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Data.Objects
{
    public class BehaviorInfo : IBehaviorData
    {
        public BehaviorInfo()
        {
            Id = Guid.NewGuid();
            Properties = new Dictionary<string, string>();
        }

        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string BehaviorType { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
