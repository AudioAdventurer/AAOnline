using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Actions.Travel
{
    public class Move :  IGameAction
    {
        public string Command => "move";
        public string CommandAlias => "go";
        public CommandCategory Category => CommandCategory.Travel;
        public string Description => "Travel from one room to another";

        public void Execute(IActionInput actionInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
