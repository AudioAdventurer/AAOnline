using AudioAdventurer.Library.Cache.Managers;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Testing.Services
{
    public class InMemoryThingService : IThingService
    {
        private readonly CacheManager<IThing> _cacheManager;
        
        public InMemoryThingService()
        {
            _cacheManager = new CacheManager<IThing>();
        }

        public IThing GetThing(Guid id)
        {
            return _cacheManager.GetItem(id);
        }

        public void SaveThing(IThing thing)
        {
            _cacheManager.SetItem(thing);
        }
    }
}
