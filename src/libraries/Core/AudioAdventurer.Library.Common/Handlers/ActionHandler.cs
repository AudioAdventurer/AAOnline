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
        private readonly IServerOutputWriter _writer;
        private readonly Dictionary<string, IGameAction> _actionsByPrimary;
        private readonly Dictionary<string, IGameAction> _actionsByAlias;

        public ActionHandler(
            IEnumerable<IGameAction> actions,
            IServerOutputWriter outputWriter)
        {
            _writer = outputWriter;

            _actions = actions.ToList();

            _actionsByPrimary = _actions.ToDictionary(
                a => a.Command);

            _actionsByAlias = _actions.ToDictionary(
                a => a.CommandAlias);
        }

        public IEnumerable<IGameAction> Actions => _actions;

        public void HandleAction(IActionInput actionInput)
        {
            IGameAction gameAction = null;

            if (_actionsByPrimary.ContainsKey(actionInput.Action))
            {
                gameAction = _actionsByPrimary[actionInput.Action];
            }
            else if (_actionsByAlias.ContainsKey(actionInput.Action))
            {
                gameAction = _actionsByAlias[actionInput.Action];
            }

            if (gameAction != null)
            {
                gameAction.Execute(actionInput, this);
            }
            else
            {
                var output = _writer.WriteUnknownCommand(actionInput.Action);
                actionInput.Session.WriteServerOutput(output);
            }
        }
    }
}
