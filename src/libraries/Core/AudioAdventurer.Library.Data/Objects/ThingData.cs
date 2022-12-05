using System;
using AudioAdventurer.Library.Common.Interfaces;
using LiteDB;

namespace AudioAdventurer.Library.Data.Objects
{
    public class ThingData : IThingData
    {
        public ThingData()
        {
            Id = Guid.NewGuid();
            MaxChildren = Int32.MaxValue;
            MaxParents = 1;
        }

        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SingularPrefix { get; set; }
        public string PluralSuffix { get; set; }
        public int MaxChildren { get; set; }
        public int MaxParents { get; set; }
    }
}
