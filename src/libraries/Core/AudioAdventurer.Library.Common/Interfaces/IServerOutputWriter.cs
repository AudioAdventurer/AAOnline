using System.Collections.Generic;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IServerOutputWriter
{
    public ServerOutput WriteHelpCommands(
        IEnumerable<IGameAction> actions);

    public ServerOutput WriteRoomDetails(
        IThing viewer,
        IThing room);

    public ServerOutput WriteThingDetails(
        IThing viewer,
        IThing viewedThing);

    public ServerOutput WriteUnknownCommand(
        string action);

    public ServerOutput WriteUnknownDirection(
        string direction);
}