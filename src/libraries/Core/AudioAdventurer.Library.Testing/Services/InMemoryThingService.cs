using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Cache.Managers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Testing.Objects;

namespace AudioAdventurer.Library.Testing.Services
{
    public class InMemoryThingService : IThingService
    {
        private readonly CacheManager<IThing> _cacheManager;
        private readonly List<IBehaviorFactory> _behaviorResolvers;
        private readonly IMessageBus _messageBus;

        public InMemoryThingService(
            IMessageBus messageBus,
            IEnumerable<IBehaviorFactory> behaviorResolvers)
        {
            _messageBus = messageBus;
            _behaviorResolvers = behaviorResolvers.ToList();

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

        public IBehaviorData GetEmptyBehaviorData()
        {
            return new InMemoryBehaviorData();
        }

        public IThingData GetEmptyThingData()
        {
            return new InMemoryThingData();
        }

        public IBehavior FindBehavior(IBehaviorData behaviorInfo)
        {
            foreach (var behaviorResolver in _behaviorResolvers)
            {
                var behavior = behaviorResolver.ConstructBehavior(
                    behaviorInfo,
                    this);

                if (behavior != null)
                {
                    return behavior;
                }
            }

            return null;
        }

        public IMessageBus GetMessageBus()
        {
            return _messageBus;
        }
    }
}
