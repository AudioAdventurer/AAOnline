using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IActionHandler
    {
        public IEnumerable<IGameAction> Actions { get; }
        public void HandleAction(IActionInput actionInput);
    }
}
