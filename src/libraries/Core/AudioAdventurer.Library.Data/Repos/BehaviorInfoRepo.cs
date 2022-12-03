using AudioAdventurer.Library.Data.Objects;
using LiteDB;
using System.Collections.Generic;
using System;
using AudioAdventurer.Library.Data.Interfaces;

namespace AudioAdventurer.Library.Data.Repos
{
    public class BehaviorInfoRepo : AbstractRepo<BehaviorData>, IBehaviorInfoRepo
    {
        public BehaviorInfoRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "Behaviors";

        public IEnumerable<BehaviorData> GetThingBehaviors(Guid thingId)
        {
            return GetMany(Query.EQ("ParentId", thingId));
        }
    }
}
