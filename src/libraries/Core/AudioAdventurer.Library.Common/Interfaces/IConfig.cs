using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IConfig
{
    public Dictionary<string, string> Values { get; }
}