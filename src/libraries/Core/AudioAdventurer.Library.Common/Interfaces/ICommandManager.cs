namespace AudioAdventurer.Library.Common.Interfaces;

public interface ICommandManager
{
    public void ExecuteAction(IActionInput action);
    public void Start(IGameManager gameManager);
    public void Stop();
}