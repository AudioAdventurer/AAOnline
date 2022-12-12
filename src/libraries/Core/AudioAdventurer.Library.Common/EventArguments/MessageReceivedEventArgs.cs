using System;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.EventArguments
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public IMessageBusMessage Message { get; set; }
    }
}
