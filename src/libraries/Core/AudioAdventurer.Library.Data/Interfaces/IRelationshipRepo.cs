using AudioAdventurer.Library.Data.Objects;
using System.Collections.Generic;
using System;

namespace AudioAdventurer.Library.Data.Interfaces;

public interface IRelationshipRepo : IRepo<RelationshipData>
{
    public IEnumerable<RelationshipData> GetChildren(Guid parentId);
    public void DeleteChildren(Guid parentId);

    public IEnumerable<RelationshipData> GetParents(Guid childId);
    public void DeleteParents(Guid childId);


}