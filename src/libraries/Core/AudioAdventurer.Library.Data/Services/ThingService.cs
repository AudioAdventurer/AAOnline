using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Cache.Managers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Repos;

namespace AudioAdventurer.Library.Data.Services
{
    public class ThingService
    {
        private readonly BehaviorInfoRepo _behaviorRepo;
        private readonly List<IBehaviorResolver> _behaviorResolvers;
        private readonly ThingInfoRepo _thingRepo;
        private readonly CacheManager<IThing> _thingCacheManager;

        public ThingService(
            BehaviorInfoRepo behaviorRepo,
            ThingInfoRepo thingRepo,
            IEnumerable<IBehaviorResolver> behaviorResolvers,
            CacheManager<IThing> thingCacheManager)
        {
            _behaviorRepo = behaviorRepo;
            _behaviorResolvers = behaviorResolvers.ToList();
            _thingRepo = thingRepo;
            _thingCacheManager = thingCacheManager;
        }

        public IThing GetThing(Guid id)
        {
            var thing = _thingCacheManager.GetItem(id);
            
            if (thing == null)
            {
                var thingInfo = _thingRepo.GetOne(id);
                var behaviorInfos = _behaviorRepo.GetChildren(thingInfo.Id);

                var behaviors = new List<IBehavior>();

                foreach (var behaviorInfo in behaviorInfos)
                {
                    var behavior = FindBehavior(behaviorInfo);

                    if (behavior != null)
                    {
                        behaviors.Add(behavior);
                    }
                }

                thing = new Thing(thingInfo, behaviors);
                _thingCacheManager.SetItem(thingInfo.Id, thing);

                if (thingInfo.ParentId.HasValue)
                {
                    thing.Parent = GetThing(thingInfo.ParentId.Value);
                }
            }

            return thing;
        }

        private IBehavior FindBehavior(IBehaviorInfo behaviorInfo)
        {
            foreach (var behaviorResolver in _behaviorResolvers)
            {
                var behavior = behaviorResolver.ResolveBehavior(behaviorInfo);

                if (behavior != null)
                {
                    behavior.SetBehaviorInfo(behaviorInfo);
                    return behavior;
                }
            }

            return null;
        }
    }
}
