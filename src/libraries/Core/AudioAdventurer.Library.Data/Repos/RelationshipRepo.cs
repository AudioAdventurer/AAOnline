using AudioAdventurer.Library.Data.Objects;
using LiteDB;
using System.Collections.Generic;
using System;
using AudioAdventurer.Library.Data.Interfaces;

namespace AudioAdventurer.Library.Data.Repos
{
    public class RelationshipRepo : AbstractRepo<RelationshipData>, IRelationshipRepo
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

        public void DeleteChildren(Guid parentId)
        {
            DeleteMany(Query.EQ("ParentId", parentId));
        }

        public void DeleteParents(Guid childId)
        {
            DeleteMany(Query.EQ("ChildId", childId));
        }
    }
}
