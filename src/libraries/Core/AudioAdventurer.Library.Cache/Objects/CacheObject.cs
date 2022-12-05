using System;

namespace AudioAdventurer.Library.Cache.Objects
{
    public class CacheObject<T>
    {
        public CacheObject(
            Guid id,
            T value)
        {
            Id = id;
            Value = value;
            CacheTime = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public T Value { get; }

        public DateTime CacheTime { get; }
    }
}
