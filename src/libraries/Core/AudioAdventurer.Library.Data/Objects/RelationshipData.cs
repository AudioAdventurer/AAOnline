using System;
using AudioAdventurer.Library.Data.Interfaces;
using LiteDB;

namespace AudioAdventurer.Library.Data.Objects
{
    public class RelationshipData : IRelationship
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
    }
}
