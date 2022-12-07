using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Actions.Inform
{
    public class Look : IGameAction
    {
        public string Command => "look";
        public string CommandAlias => "l";
        public CommandCategory Category => CommandCategory.Inform;
        public string Description => "Look at the room, item, person, or monster.";
        

        public void Execute(ActionInput actionInput)
        {
                
        }
    }
}
