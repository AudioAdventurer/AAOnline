using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class ActionEnqueuedEventArgs
        : EventArgs
    {
        public IActionInput Action { get; set; }
    }
}
