using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class ServerOutputReceivedEventArgs
        : EventArgs
    {
        public string ServerOutput { get; set; }
        public ISession Session { get; set; }
    }
}
