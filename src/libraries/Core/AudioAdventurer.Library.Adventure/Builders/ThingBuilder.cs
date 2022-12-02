using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Objects;

namespace AudioAdventurer.Library.Adventure.Builders
{
    public static class ThingBuilder
    {
        public static IThing BuildRoom(
            string name,
            string description = null,
            string fullName = null)
        {
            var roomInfo = new ThingData()
            {
                Name = name,
                Description = description,
                FullName = fullName
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
                behaviors);

            return room;
        }

        public static IThing BuildExit(
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
            exitBehavior = new ExitBehavior(behaviorInfo);

            List<IBehavior> behaviors = new List<IBehavior> { exitBehavior };
            var exit = new Thing(exitInfo, behaviors);

            return exit;
        }
    }
}
