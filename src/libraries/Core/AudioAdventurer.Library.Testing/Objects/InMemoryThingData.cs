using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Testing.Objects
{
    public class InMemoryThingData : IThingData
    {
        public InMemoryThingData()
        {
            Id = Guid.NewGuid();
            MaxParents = 1;
            MaxChildren = Int32.MaxValue;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SingularPrefix { get; set; }
        public string PluralSuffix { get; set; }
        public int MaxChildren { get; set; }
        public int MaxParents { get; set; }
    }
}
