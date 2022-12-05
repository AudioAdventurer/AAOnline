using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class SessionRemovedEventArgs 
        : EventArgs
    {
        public ISession Session { get; set; }
    }
}
