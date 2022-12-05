using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Cache.Objects;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Cache.Managers
{
    public class CacheManager<T>: ICacheManager<T>
        where T : IIdentifiableObject
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

        public void SetItem(T obj)
        {
            var cacheObject = new CacheObject<T>(obj.Id, obj);
            _cache[obj.Id] = cacheObject;
        }
    }
}
