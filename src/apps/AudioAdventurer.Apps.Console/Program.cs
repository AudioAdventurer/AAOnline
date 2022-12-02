using AudioAdventurer.Library.Adventure.Builders;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Data.Objects;

namespace AudioAdventurer.Apps.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var roomA = ThingBuilder.BuildRoom("Room A");
            var roomB = ThingBuilder.BuildRoom("Room B");
            
            var exit = ThingBuilder.BuildExit(
                "Exit",
                out ExitBehavior exitBehavior);

            roomA.AddChild(exit);
            exitBehavior.AddDestination("east", roomB.Info.Id);

            
        }
    }
}
