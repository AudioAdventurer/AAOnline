using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Events
{
    public class AddChildEvent
        : CancellableGameEvent
    {
        public AddChildEvent(IThing activeThing, IThing newParent)
            : base(activeThing, null)
        {
            NewParent = newParent;
        }

        /// <summary>Gets the new parent.</summary>
        /// <value>The new parent.</value>
        public IThing NewParent { get; private set; }
    }
}
