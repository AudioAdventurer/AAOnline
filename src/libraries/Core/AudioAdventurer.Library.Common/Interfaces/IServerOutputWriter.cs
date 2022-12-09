using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IServerOutputWriter
{
    public ServerOutput WriteThingDetails(
        IThing viewer,
        IThing viewedThing);

    public ServerOutput WriteRoomDetails(
        IThing viewer,
        IThing room);
}