using AudioAdventurer.GameExtensions.SampleAdventure.Behaviors;
using AudioAdventurer.Library.Adventure.Builders;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.GameExtensions.SampleAdventure.Builders
{
    public static class SampleAdventureBuilder
    {
        public static IThing BuildAdventure(
            IThingService thingService)
        {
            var world = thingService.BuildWorld("The World", "The world we live in");

            var area = thingService.BuildArea("Area one", "Area one of the world");
            area.AddParent(world);

            var entrance = thingService.BuildRoom("Cave Entrance", "You are standing outside of a cave entrance.");
            entrance.AddParent(area);

            var tunnel = thingService.BuildRoom("You are in a dark tunnel that heads further down or back up", "dark tunnel");

            var caveEntrance = thingService.BuildExit("Cave Entrance", out IBehavior exitBehavior);
            if (exitBehavior is ExitBehavior eb)
            {
                caveEntrance.AddParent(entrance);
                caveEntrance.AddParent(tunnel);
                eb.AddDestination(DirectionConstants.Enter, tunnel.Id);
                eb.AddDestination(DirectionConstants.Exit, entrance.Id);
            }

            var cavern1 = thingService.BuildRoom("Large Cavern", "You are in a large cavern.");
            var cavern1Entrance = thingService.BuildExit("Cavern Entrance", out exitBehavior);
            if (exitBehavior is ExitBehavior eb1)
            {
                cavern1Entrance.AddParent(tunnel);
                cavern1Entrance.AddParent(cavern1);
                eb1.AddDestination(DirectionConstants.East, tunnel.Id);
                eb1.AddDestination(DirectionConstants.West, cavern1.Id);
            }

            var cavernRoom = thingService.BuildRoom("Small room", "You are in a small room that appears to be a workshop.");
            var cavernRoomEntrance = thingService.BuildExit("Door", out exitBehavior);
            if (exitBehavior is ExitBehavior eb2)
            {
                cavernRoomEntrance.AddParent(cavern1);
                cavernRoomEntrance.AddParent(cavernRoom);
                eb2.AddDestination(DirectionConstants.Down, cavernRoom.Id);
                eb2.AddDestination(DirectionConstants.Up, cavern1.Id);
            }


            var parrot = thingService.BuildObject("Parrot Statue",
                "A carved statue of a parrot fashioned out of white marble.");
            parrot.AddParent(cavernRoom);

            var parrotBehaviorData = thingService.GetEmptyBehaviorData();
            var parrotBehavior = new ParrotBehavior(
                parrotBehaviorData,
                thingService);
            parrot.AddBehavior(parrotBehavior);

            var observantBehaviorData = thingService.GetEmptyBehaviorData();
            observantBehaviorData.BehaviorType = nameof(ObservantBehavior);
            var observantBehavior = thingService.FindBehavior(observantBehaviorData);
            parrot.AddBehavior(observantBehavior);
            
            return entrance;
        }
    }
}
