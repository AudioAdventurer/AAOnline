using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Handlers
{
    /// <summary>
    /// Used by the Command Manager to handle and dispatch
    /// game actions.
    /// </summary>
    public class ActionHandler : IActionHandler
    {
        private readonly List<IGameAction> _actions;
        private Dictionary<string, IGameAction> _actionsByPrimary;
        private Dictionary<string, IGameAction> _actionsByAlias;

        public ActionHandler(
            IEnumerable<IGameAction> actions)
        {
            _actions = actions.ToList();

            _actionsByPrimary = _actions.ToDictionary(
                a => a.Command);

            _actionsByAlias = _actions.ToDictionary(
                a => a.CommandAlias);
        }

        public void HandleAction(IActionInput actionInput)
        {

        }
    }
}
