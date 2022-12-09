using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Actions.Inform
{
    public class Look : IGameAction
    {
        private readonly IServerOutputWriter _writer;

        public Look(
            IServerOutputWriter writer)
        {
            _writer = writer;
        }

        public string Command => "look";
        public string CommandAlias => "l";
        public CommandCategory Category => CommandCategory.Inform;
        public string Description => "Look at the room, item, person, or monster.";

        public void Execute(IActionInput actionInput)
        {
            var session = actionInput.Session;
            if (session == null)
            {
                return;
            }

            if (TryLookAtThing(
                    actionInput, 
                    out var serverOutput))
            {
                session.WriteServerOutput(serverOutput);
                return;
            }

            serverOutput = LookAtRoom(actionInput);
            session.WriteServerOutput(serverOutput);
        }

        private bool TryLookAtThing(
            IActionInput actionInput, 
            out IServerOutput serverOutput)
        {
            serverOutput = new ServerOutput();

            if (actionInput.Tail == null)
            {
                return false;
            }

            var room = actionInput.Actor.FindParentRoom();
            if (room != null)
            {
                var thing = room.FindChild(actionInput.Tail);

                if (thing != null)
                {
                    _writer.WriteThingDetails(
                        actionInput.Actor,
                        thing);

                    return true;
                }
            }

            return false;
        }

        private IServerOutput LookAtRoom(
            IActionInput actionInput)
        {
            var room = actionInput.Actor.FindParentRoom();

            return _writer.WriteRoomDetails(
                actionInput.Actor,
                room);
        }
    }
}
