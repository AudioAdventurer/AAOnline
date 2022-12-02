using AudioAdventurer.Library.Data.Objects;
using LiteDB;
using System.Collections.Generic;
using System;

namespace AudioAdventurer.Library.Data.Repos
{
    public class RelationshipRepo : AbstractRepo<RelationshipData>
    {
        public RelationshipRepo(LiteDatabase db) 
            : base(db)
        {
            Collection.EnsureIndex("ParentId", false);
            Collection.EnsureIndex("ChildId", false);
        }

        public override string CollectionName => "Relationships";

        public IEnumerable<RelationshipData> GetChildren(Guid parentId)
        {
            return GetMany(Query.EQ("ParentId", parentId));
        }

        public IEnumerable<RelationshipData> GetParents(Guid childId)
        {
            return GetMany(Query.EQ("ChildId", childId));
        }
    }
}
