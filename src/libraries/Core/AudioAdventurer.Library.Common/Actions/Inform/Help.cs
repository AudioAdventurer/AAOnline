using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Actions.Inform
{
    public class Help : IGameAction
    {
        private readonly IServerOutputWriter _writer;

        public Help(IServerOutputWriter writer)
        {
            _writer = writer;
        }

        public string Command => "help";
        public string CommandAlias => "h";
        public CommandCategory Category => CommandCategory.Inform;
        public string Description => "Provides user with information on commands.";

        public void Execute(
            IActionInput actionInput,
            IActionHandler actionHandler)
        {
            var session = actionInput.Session;

            var actions = actionHandler.Actions.ToList();
            var sorted = actions
                .OrderBy(a => a.Command)
                .ToList();

            var output = _writer.WriteHelpCommands(sorted);
            session.WriteServerOutput(output);
        }
    }
}
