using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IServerOutput
{
    public List<IServerOutputEntry> OutputEntries { get; }
}