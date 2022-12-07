using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class ServerOutputReceivedEventArgs
        : EventArgs
    {
        public IServerOutput ServerOutput { get; set; }
        public ISession Session { get; set; }
    }
}
