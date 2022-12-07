using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ServerOutput : IServerOutput
    {
        public ServerOutput()
        {
            OutputEntries = new List<IServerOutputEntry>();
        }

        public List<IServerOutputEntry> OutputEntries { get; }
    }
}
