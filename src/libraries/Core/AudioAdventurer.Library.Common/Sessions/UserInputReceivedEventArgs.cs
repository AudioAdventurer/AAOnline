using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Sessions
{
    public class UserInputReceivedEventArgs 
        : EventArgs
    {
        public string Command { get; set; }
        public ISession Session { get; set; }
    }
}
