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
        public ServerOutput WriteHelpCommands(
            IEnumerable<IGameAction> actions)
        {
            var serverOutput = new ServerOutput();

            serverOutput.AppendEntry(
                ServerOutputDataTypes.ListHeader,
                "Available commands:",
                true);

            foreach (var action in actions)
            {
                string text = $"{action.Command} ({action.CommandAlias}): {action.Description}";

                serverOutput.AppendEntry(
                    ServerOutputDataTypes.ListItem,
                    text,
                    true);
            }

            return serverOutput;
        }

        public ServerOutput WriteUnknownCommand(
            string action)
        {
            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Action,
                action,
                false);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                "is an unknown command.",
                true);

            return serverOutput;
        }

        public ServerOutput WriteUnknownDirection(string direction)
        {
            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                "Unable to move in specified direction of",
                false);

            serverOutput.AppendEntry(
                ServerOutputDataTypes.Direction,
                direction,
                false);

            return serverOutput;
        }

        public ServerOutput WriteSayOutput(string statement)
        {
            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry(
                ServerOutputDataTypes.Text,
                $"You say: \"{statement}\"",
                true);
            return serverOutput;
        }

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
            var things = new List<string>();

            var children = room.GetChildren();
            foreach (var child in children)
            {
                var exitBehavior = child.FindBehavior<ExitBehavior>();
                if (exitBehavior != null)
                {
                    exits.Add(exitBehavior.GetExitCommandFrom(room));
                }
                else
                {
                    if (!child.Id.Equals(viewer.Id))
                    {
                        things.Add(child.Name);
                    }
                }

            }

            if (exits.Any())
            {
                serverOutput.AppendEntry(
                    ServerOutputDataTypes.Notice,
                    "Here you notice:",
                    true);

                if (exits.Any())
                {
                    serverOutput.AppendEntry(
                        ServerOutputDataTypes.Text,
                        $"  Routes: {string.Join(", ", exits)}",
                        true);
                }

                if (things.Any())
                {
                    serverOutput.AppendEntry(
                        ServerOutputDataTypes.Text,
                        $"  Things: {string.Join(", ", things)}",
                        true);
                }

                serverOutput.AppendEmptyLine();
            }

            return serverOutput;
        }
    }
}
