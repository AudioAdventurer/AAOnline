using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IGameAction
{
    public string Command { get; }

    public string CommandAlias { get; }

    public CommandCategory Category { get; }

    public string Description { get; }
    
    public void Execute(ActionInput actionInput);
}