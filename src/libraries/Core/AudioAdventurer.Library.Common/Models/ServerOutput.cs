using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ServerOutput : IServerOutput
    {
        public ServerOutput()
        {
            Entries = new List<IServerOutputEntry>();
        }

        public List<IServerOutputEntry> Entries { get; }
    }
}
