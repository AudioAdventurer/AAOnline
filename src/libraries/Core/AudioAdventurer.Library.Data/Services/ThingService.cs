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
        private readonly RelationshipRepo _relationshipRepo;
        private readonly ThingInfoRepo _thingRepo;

        private readonly List<IBehaviorResolver> _behaviorResolvers;
        
        private readonly CacheManager<IThing> _thingCacheManager;

        public ThingService(
            BehaviorInfoRepo behaviorRepo,
            RelationshipRepo relationshipRepo,
            ThingInfoRepo thingRepo,
            IEnumerable<IBehaviorResolver> behaviorResolvers,
            CacheManager<IThing> thingCacheManager)
        {
            _behaviorRepo = behaviorRepo;
            _relationshipRepo = relationshipRepo;
            _thingRepo = thingRepo;

            _behaviorResolvers = behaviorResolvers.ToList();
            _thingCacheManager = thingCacheManager;
        }

        public IThing GetThing(Guid id)
        {
            var thing = _thingCacheManager.GetItem(id);
            
            if (thing == null)
            {
                var thingInfo = _thingRepo.GetOne(id);
                var behaviorInfos = _behaviorRepo.GetThingBehaviors(thingInfo.Id);

                var behaviors = new List<IBehavior>();

                foreach (var behaviorInfo in behaviorInfos)
                {
                    var behavior = FindBehavior(behaviorInfo);

                    if (behavior != null)
                    {
                        behaviors.Add(behavior);
                    }
                }

                Lazy<List<IThing>> parents = new Lazy<List<IThing>>(
                    () => this.GetParents(thingInfo.Id).ToList());

                Lazy<List<IThing>> children = new Lazy<List<IThing>>(
                    () => this.GetChildren(thingInfo.Id).ToList());

                thing = new Thing(
                    thingInfo, 
                    behaviors,
                    parents,
                    children);
                _thingCacheManager.SetItem(thingInfo.Id, thing);
            }

            return thing;
        }

        public IEnumerable<IThing> GetParents(Guid childId)
        {
            var output = new List<IThing>();

            var parents = _relationshipRepo.GetParents(childId);

            foreach (var parent in parents)
            {
                var parentThing = GetThing(parent.Id);
                output.Add(parentThing);
            }

            return output;
        }

        public IEnumerable<IThing> GetChildren(Guid parentId)
        {
            var output = new List<IThing>();

            var children = _relationshipRepo.GetChildren(parentId);

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
                    return behavior;
                }
            }

            return null;
        }
    }
}
