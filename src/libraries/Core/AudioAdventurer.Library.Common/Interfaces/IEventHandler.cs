namespace AudioAdventurer.Library.Common.Interfaces;

public interface IEventHandler
{
    public void SendMessage(IGameEvent gameEvent);
}