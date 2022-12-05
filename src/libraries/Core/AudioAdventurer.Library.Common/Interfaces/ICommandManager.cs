namespace AudioAdventurer.Library.Common.Interfaces;

public interface ICommandManager
{
    public void Start(IGameManager gameManager);
    public void Stop();
}