namespace AudioAdventurer.Library.Common.Interfaces;

public interface IGameManager
{
    public void Start();
    public void Stop();
    
    // Session Management
    public void AddSession(ISession session);
    public void RemoveSession(ISession session);

    //Command Management
    public void EnqueueCommand(IActionInput actionInput);


}