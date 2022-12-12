using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Interfaces;
using AudioAdventurer.Library.Data.Objects;

namespace AudioAdventurer.Library.Data.Services
{
    public class ThingService : IThingService
    {
        private readonly IBehaviorInfoRepo _behaviorRepo;
        private readonly IRelationshipRepo _relationshipRepo;
        private readonly IThingInfoRepo _thingRepo;

        private readonly List<IBehaviorFactory> _behaviorResolvers;
        
        private readonly ICacheManager<IThing> _thingCacheManager;
        private readonly IMessageBus _messageBus;

        public ThingService(
            IBehaviorInfoRepo behaviorRepo,
            IRelationshipRepo relationshipRepo,
            IThingInfoRepo thingRepo,
            IEnumerable<IBehaviorFactory> behaviorResolvers,
            ICacheManager<IThing> thingCacheManager,
            IMessageBus messageBus)
        {
            _behaviorRepo = behaviorRepo;
            _relationshipRepo = relationshipRepo;
            _thingRepo = thingRepo;

            _behaviorResolvers = behaviorResolvers.ToList();
            _thingCacheManager = thingCacheManager;
            _messageBus = messageBus;
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

                var parentIds = this.GetParentIds(thingInfo.Id);
                var childIds = this.GetChildIds(thingInfo.Id);

                thing = new Thing(
                    thingInfo,
                    behaviors,
                    parentIds,
                    childIds,
                    this);

                _thingCacheManager.SetItem(thing);
            }

            return thing;
        }

        public void SaveThing(IThing thing)
        {
            if (thing.GetThingData() is ThingData thingData)
            {
                // Save the Thing Data
                _thingRepo.Save(thingData);

                // Save the Behavior Data
                var behaviors = thing.BehaviorHandler.AllBehaviors;
                foreach (var behavior in behaviors)
                {
                    if (behavior.GetProperties() is BehaviorData behaviorData)
                    {
                        _behaviorRepo.Save(behaviorData);
                    }
                }

                // Remove any existing relationships.
                // Not trying to match up at moment
                _relationshipRepo.DeleteParents(thingData.Id);
                _relationshipRepo.DeleteChildren(thingData.Id);

                // Now save back parents
                foreach (var parentId in thing.Parents)
                {
                    var relationship = new RelationshipData
                    {
                        ParentId = parentId,
                        ChildId = thingData.Id
                    };

                    _relationshipRepo.Save(relationship);
                }

                // Now save back children
                foreach (var childId in thing.Children)
                {
                    var relationship = new RelationshipData
                    {
                        ParentId = thingData.Id,
                        ChildId = childId
                    };

                    _relationshipRepo.Save(relationship);
                }
            }
        }

        private IEnumerable<Guid> GetParentIds(Guid childId)
        {
            var output = new List<Guid>();

            var parents = _relationshipRepo.GetParents(childId);

            foreach (var parent in parents)
            {
                output.Add(parent.Id);
            }

            return output;
        }

        private IEnumerable<Guid> GetChildIds(Guid parentId)
        {
            var output = new List<Guid>();

            var children = _relationshipRepo.GetChildren(parentId);

            foreach (var child in children)
            {
                output.Add(child.ChildId);
            }

            return output;
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

        public IBehaviorData GetEmptyBehaviorData()
        {
            return new BehaviorData();
        }

        public IThingData GetEmptyThingData()
        {
            return new ThingData();
        }
    }
}
