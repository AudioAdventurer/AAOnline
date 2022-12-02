using AudioAdventurer.Library.Data.Objects;
using LiteDB;

namespace AudioAdventurer.Library.Data.Repos
{
    public class ThingInfoRepo : AbstractRepo<ThingData>
    {
        public ThingInfoRepo(LiteDatabase db) 
            : base(db)
        {
        }

        public override string CollectionName => "Things";
    }
}
