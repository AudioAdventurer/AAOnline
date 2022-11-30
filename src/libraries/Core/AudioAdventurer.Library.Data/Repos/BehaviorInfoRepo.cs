using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Data.Objects;
using LiteDB;

namespace AudioAdventurer.Library.Data.Repos
{
    public class BehaviorInfoRepo : AbstractRepo<BehaviorInfo>
    {
        public BehaviorInfoRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "Behaviors";
    }
}
