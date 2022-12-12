using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Adventure.Builders
{
    public static class ThingBuilder
    {
        public static IThing BuildWorld(
            this IThingService thingService,
            string name,
            string description)
        {
            var worldInfo = thingService.GetEmptyThingData();
            worldInfo.Name = name;
            worldInfo.Description = description;

            var behaviorInfo = thingService.GetEmptyBehaviorData();
            behaviorInfo.BehaviorType = nameof(WorldBehavior);
            behaviorInfo.ParentId = worldInfo.Id;
            var worldBehavior = thingService.FindBehavior(behaviorInfo);

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
            var areaInfo = thingService.GetEmptyThingData();
            areaInfo.Name = name;
            areaInfo.Description = description;

            var behaviorInfo = thingService.GetEmptyBehaviorData();
            behaviorInfo.BehaviorType = nameof(AreaBehavior);
            behaviorInfo.ParentId = areaInfo.Id;
            var areaBehavior = thingService.FindBehavior(behaviorInfo);

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

        public static IThing BuildObject(
            this IThingService thingService,
            string name,
            string description = null)
        {
            var objectInfo = thingService.GetEmptyThingData();
            objectInfo.Name = name;
            objectInfo.Description = description;

            var obj = new Thing(
                objectInfo,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(obj);

            return obj;
        }

        public static IThing BuildRoom(
            this IThingService thingService,
            string name,
            string description = null)
        {
            var roomInfo = thingService.GetEmptyThingData();
            roomInfo.Name = name;
            roomInfo.Description = description;

            var behaviorInfo = thingService.GetEmptyBehaviorData();
            behaviorInfo.BehaviorType = nameof(RoomBehavior);
            behaviorInfo.ParentId = roomInfo.Id;
            var roomBehavior = thingService.FindBehavior(behaviorInfo);

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
            var movableItemData = thingService.GetEmptyThingData();
            movableItemData.Name = name;
            movableItemData.Description = description;

            var behaviorInfo = thingService.GetEmptyBehaviorData();
            behaviorInfo.BehaviorType = nameof(MovableBehavior);
            behaviorInfo.ParentId = movableItemData.Id;
            var movableBehavior = thingService.FindBehavior(behaviorInfo);

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
            out IBehavior exitBehavior)
        {
            var exitInfo = thingService.GetEmptyThingData();
            exitInfo.Name = name;
            exitInfo.MaxParents = 2;

            var behaviorInfo = thingService.GetEmptyBehaviorData();
            behaviorInfo.BehaviorType = nameof(ExitBehavior);
            behaviorInfo.ParentId = exitInfo.Id;
            exitBehavior = thingService.FindBehavior(behaviorInfo);

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

        public static IThing BuildPlayer(
            this IThingService thingService,
            string name,
            out IBehavior playerBehavior)
        {
            var playerInfo = thingService.GetEmptyThingData();
            playerInfo.Name = name;
            playerInfo.MaxParents = 1;

            var playerBehaviorInfo = thingService.GetEmptyBehaviorData();
            playerBehaviorInfo.BehaviorType = nameof(PlayerBehavior);
            playerBehaviorInfo.ParentId = playerInfo.Id;
            playerBehavior = thingService.FindBehavior(playerBehaviorInfo);

            var moveableBehaviorInfo = thingService.GetEmptyBehaviorData();
            moveableBehaviorInfo.BehaviorType = nameof(MovableBehavior);
            moveableBehaviorInfo.ParentId = playerInfo.Id;
            var moveable = thingService.FindBehavior(moveableBehaviorInfo);

            var userSensoryBehaviorInfo = thingService.GetEmptyBehaviorData();
            userSensoryBehaviorInfo.BehaviorType = nameof(UserSensoryBehavior);
            userSensoryBehaviorInfo.ParentId = playerInfo.Id;
            var userSensoryBehavior = thingService.FindBehavior(userSensoryBehaviorInfo);

            var observantBehaviorInfo = thingService.GetEmptyBehaviorData();
            observantBehaviorInfo.BehaviorType = nameof(ObservantBehavior);
            observantBehaviorInfo.ParentId = playerInfo.Id;
            var observantBehavior = thingService.FindBehavior(observantBehaviorInfo);

            List<IBehavior> behaviors = new List<IBehavior>
            {
                playerBehavior,
                moveable,
                observantBehavior,
                userSensoryBehavior
            };

            var player = new Thing(
                playerInfo,
                behaviors,
                new List<Guid>(),
                new List<Guid>(),
                thingService);

            thingService.SaveThing(player);

            return player;
        }
    }
}
