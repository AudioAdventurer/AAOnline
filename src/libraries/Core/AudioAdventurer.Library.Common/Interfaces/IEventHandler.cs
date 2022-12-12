namespace AudioAdventurer.Library.Common.Interfaces;

public interface IEventHandler
{
    public void SendMessage(IGameEvent gameEvent);
    public void SendCommandMessage(string command, IThing actor);
}