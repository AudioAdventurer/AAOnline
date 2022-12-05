using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IGameManager
{
    public event EventHandler GameManagerStarted;
    public event EventHandler GameManagerStopped;

    public event EventHandler SessionAdded;
    public event EventHandler SessionRemoved;

    public event EventHandler ActionEnqueued;
    public event EventHandler ActionDequeued;

    public void Start();
    public void Stop();

    public bool Running { get; }
    
    // Session Management
    public bool AddSession(ISession session);
    public void RemoveSession(ISession session);

    //Command Management
    public bool EnqueueAction(IActionInput actionInput);
    public IActionInput DequeueAction();
}