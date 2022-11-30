using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Data.Objects
{
    public class ThingInfo : IThingInfo
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; }
        public string FullName { get; set; }
        public string Description { get; }
        public string Title { get; }
    }
}
