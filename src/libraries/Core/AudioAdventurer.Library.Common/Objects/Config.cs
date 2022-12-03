using System.Collections.Generic;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Objects
{
    public class Config : IConfig
    {
        public Config()
        {
            Values = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Values { get; }
    }
}
