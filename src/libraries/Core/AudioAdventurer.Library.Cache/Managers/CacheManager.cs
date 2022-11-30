using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Cache.Objects;

namespace AudioAdventurer.Library.Cache.Managers
{
    public class CacheManager<T>
    {
        private readonly Dictionary<Guid, CacheObject<T>> _cache;

        public CacheManager()
        {
            _cache = new Dictionary<Guid, CacheObject<T>>();
        }

        public T GetItem(Guid id)
        {
            if (_cache.ContainsKey(id))
            {
                var obj = _cache[id].Value;
                return obj;
            }

            return default(T);
        }

        public void SetItem(Guid id, T obj)
        {
            var cacheObject = new CacheObject<T>(id, obj);
            _cache[id] = cacheObject;
        }
    }
}
