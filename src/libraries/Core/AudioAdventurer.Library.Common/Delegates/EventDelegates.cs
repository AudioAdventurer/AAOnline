using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Delegates
{
    public delegate void GameEventHandler(
        Thing root, 
        AbstractGameEvent e);

    public delegate void CancellableGameEventHandler(
        Thing root, 
        CancellableGameEvent e);
}
