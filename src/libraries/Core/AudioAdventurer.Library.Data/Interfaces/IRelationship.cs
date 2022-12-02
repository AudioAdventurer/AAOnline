using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Data.Interfaces;

public interface IRelationship : IIdentifiableObject
{
    public Guid ParentId { get; set; }

    public Guid ChildId { get; set; }
}