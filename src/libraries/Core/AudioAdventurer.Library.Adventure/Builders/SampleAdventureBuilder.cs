using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Adventure.Builders
{
    public static class SampleAdventureBuilder
    {
        public static IThing BuildAdventure(
            IThingService _thingService)
        {
            var world = _thingService.BuildWorld("The World", "The world we live in");

            var area = _thingService.BuildArea("Area one", "Area one of the world");
            area.AddParent(world);

            var entrance = _thingService.BuildRoom("Cave Entrance", "You are standing outside of a cave entrance.");
            entrance.AddParent(area);

            var tunnel = _thingService.BuildRoom("You are in a dark tunnel that heads further down or back up", "dark tunnel");

            var caveEntrance = _thingService.BuildExit("Cave Entrance", out ExitBehavior exitBehavior);
            caveEntrance.AddParent(entrance);
            caveEntrance.AddParent(tunnel);
            exitBehavior.AddDestination("Enter", tunnel.Id);
            exitBehavior.AddDestination("Exit", entrance.Id);

            var cavern1 = _thingService.BuildRoom("Large Cavern", "You are in a large cavern.");
            var cavern1Entrance = _thingService.BuildExit("Cavern Entrance", out ExitBehavior exit1Behavior);
            cavern1Entrance.AddParent(tunnel);
            cavern1Entrance.AddParent(cavern1);
            exitBehavior.AddDestination("E", tunnel.Id);
            exitBehavior.AddDestination("W", cavern1.Id);

            return entrance;
        }
    }
}
