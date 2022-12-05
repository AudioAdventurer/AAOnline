using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;
using LiteDB;

namespace AudioAdventurer.Library.Data.Objects
{
    public class BehaviorData : IBehaviorData
    {
        public BehaviorData()
        {
            Id = Guid.NewGuid();
            Properties = new Dictionary<string, string>();
        }

        [BsonId]
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public string BehaviorType { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
