using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class SessionAddedEventArgs 
        : EventArgs
    {
        public ISession Session { get; set; }
    }
}
