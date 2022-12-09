using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Writers
{
    public class ServerOutputWriter : IServerOutputWriter
    {
        public ServerOutput WriteThingDetails(
            IThing viewer, 
            IThing viewedThing)
        {
            // TODO - Can viewer see the thing

            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                "You examine",
                false);
            
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Name,
                viewedThing.Name,
                false);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                ":",
                false);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Description,
                viewedThing.Description,
                true);

            return serverOutput;
        }

        public ServerOutput WriteRoomDetails(
            IThing viewer, 
            IThing room)
        {
            // TODO - can the viewer see the room

            var serverOutput = new ServerOutput();

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Name,
                room.Name,
                true);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Description,
                room.Description,
                true);

            var exits = new List<string>();

            var children = room.GetChildren();
            foreach (var child in children)
            {
                var exitBehavior = child.FindBehavior<ExitBehavior>();
                if (exitBehavior != null)
                {
                    exits.Add(exitBehavior.GetExitCommandFrom(room));
                }
            }

            if (exits.Any())
            {
                serverOutput.AppendEntry(
                    ServerOutputDataTypes.Notice,
                    "Here you notice:",
                    true);

                serverOutput.AppendEntry(
                    ServerOutputDataTypes.Text,
                    $"  Routes: {string.Join(", ", exits)}",
                    true);
            }

            return serverOutput;
        }
    }
}
