using System;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IIdentifiableObject
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
    }
}
