using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Cache.Managers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Repos;

namespace AudioAdventurer.Library.Data.Services
{
    public class ThingService : IThingService
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

                Lazy<IThing> parent = null;
                
                if (thingInfo.ParentId.HasValue)
                {
                    parent = new Lazy<IThing>(
                        () => this.GetThing(thingInfo.ParentId.Value), true);
                }

                Lazy<IEnumerable<IThing>> children = new Lazy<IEnumerable<IThing>>(
                    () => this.GetChildren(thingInfo.Id));

                thing = new Thing(
                    thingInfo, 
                    behaviors,
                    parent,
                    children);
                _thingCacheManager.SetItem(thingInfo.Id, thing);
            }

            return thing;
        }

        public IEnumerable<IThing> GetChildren(Guid parentId)
        {
            var output = new List<IThing>();

            var children = _thingRepo.GetChildren(parentId);

            foreach (var child in children)
            {
                var childThing = GetThing(child.Id);
                output.Add(childThing);
            }

            return output;
        }

        private IBehavior FindBehavior(IBehaviorData behaviorInfo)
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
