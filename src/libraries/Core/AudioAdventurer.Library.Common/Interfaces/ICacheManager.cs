using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface ICacheManager<T>
    where T : IIdentifiableObject
{
    public T GetItem(Guid id);
    public void SetItem(T obj);
}