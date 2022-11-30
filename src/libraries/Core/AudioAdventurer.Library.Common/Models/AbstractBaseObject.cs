using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public abstract class AbstractBaseObject : IIdentifiableObject
    {
        protected AbstractBaseObject()
        {
            Id = Guid.NewGuid();
            Lock = new object();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        internal object Lock { get; }
    }
}
