using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class UserInputReceivedEventArgs
        : EventArgs
    {
        public string Command { get; set; }
        public ISession Session { get; set; }
        public IThing Actor { get; set; }
    }
}
