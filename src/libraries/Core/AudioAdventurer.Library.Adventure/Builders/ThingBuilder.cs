using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Objects;

namespace AudioAdventurer.Library.Adventure.Builders
{
    public static class ThingBuilder
    {
        public static IThing BuildWorld(
            this IThingService thingService,
            string name,
            string description)
        {
            var worldInfo = new ThingData()
            {
                Name = name,
                Description = description
            };

            var behaviorInfo = new BehaviorData
            {
                BehaviorType = nameof(WorldBehavior),
                ParentId = worldInfo.Id
            };

            var worldBehavior = new WorldBehavior(behaviorInfo);
            List<IBehavior> behaviors = new List<IBehavior> { worldBehavior };

            var world = new Thing(
                worldInfo,
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(world);

            return world;
        }

        public static IThing BuildArea(
            this IThingService thingService,
            string name,
            string description)
        {
            var areaInfo = new ThingData()
            {
                Name = name,
                Description = description
            };

            var behaviorInfo = new BehaviorData
            {
                BehaviorType = nameof(AreaBehavior),
                ParentId = areaInfo.Id
            };

            var areaBehavior = new AreaBehavior(behaviorInfo);
            List<IBehavior> behaviors = new List<IBehavior> { areaBehavior };

            var area = new Thing(
                areaInfo,
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(area);

            return area;
        }

        public static IThing BuildRoom(
            this IThingService thingService,
            string name,
            string description = null)
        {
            var roomInfo = new ThingData()
            {
                Name = name,
                Description = description,
            };

            var behaviorInfo = new BehaviorData
            {
                BehaviorType = nameof(RoomBehavior),
                ParentId = roomInfo.Id
            };

            var roomBehavior = new RoomBehavior(behaviorInfo);

            List<IBehavior> behaviors = new List<IBehavior> { roomBehavior };

            var room = new Thing(
                roomInfo,
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(room);

            return room;
        }

        public static IThing BuildMoveableItem(
            this IThingService thingService,
            string name,
            string description = null)
        {
            var movableItemData = new ThingData()
            {
                Name = name,
                Description = description
            };

            var behaviorInfo = new BehaviorData
            {
                BehaviorType = nameof(MovableBehavior),
                ParentId = movableItemData.Id
            };

            var movableBehavior = new MovableBehavior(behaviorInfo, thingService);

            List<IBehavior> behaviors = new List<IBehavior> { movableBehavior };

            var moveableItem = new Thing(
                movableItemData,
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(moveableItem);

            return moveableItem;
        }

        public static IThing BuildExit(
            this IThingService thingService,
            string name,
            out ExitBehavior exitBehavior)
        {
            var exitInfo = new ThingData
            {
                Name = name,
                MaxParents = 2
            };

            var behaviorInfo = new BehaviorData
            {
                BehaviorType = nameof(ExitBehavior),
                ParentId = exitInfo.Id
            };
            exitBehavior = new ExitBehavior(behaviorInfo, thingService);

            List<IBehavior> behaviors = new List<IBehavior> { exitBehavior };
            var exit = new Thing(
                exitInfo, 
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(exit);

            return exit;
        }
    }
}
