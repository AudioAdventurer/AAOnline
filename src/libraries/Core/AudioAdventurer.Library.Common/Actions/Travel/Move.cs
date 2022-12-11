using System;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Actions.Travel
{
    public class Move :  IGameAction
    {
        private readonly IServerOutputWriter _writer;

        public Move(
            IServerOutputWriter writer)
        {
            _writer = writer;
        }

        public string Command => "move";
        public string CommandAlias => "go";
        public CommandCategory Category => CommandCategory.Travel;
        public string Description => "Travel from one room to another";

        public void Execute(
            IActionInput actionInput,
            IActionHandler actionHandler)
        {
            var actor = actionInput.Actor;
            var whereToGo = actionInput.Tail.Trim();
            var session = actionInput.Session;

            var room = actor.FindParentRoom();

            if (room != null)
            {
                var children = room.GetChildren();

                foreach (var child in children)
                {
                    var exits = child.FindBehaviors<ExitBehavior>();

                    foreach (var exit in exits)
                    {
                        var exitCommand = exit.GetExitCommandFrom(room);
                        if (exitCommand.Equals(whereToGo, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (exit.MoveThrough(actor))
                            {
                                actionHandler.HandleAction(new
                                    ActionInput("look", session, actor));

                                return;
                            }
                            else
                            {
                                // TODO - Need to figure out how to say why
                            }
                        }
                    }
                }
            }

            session.WriteServerOutput(
                _writer.WriteUnknownDirection(whereToGo));
        }
    }
}
