using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Constants
{
    public static class DirectionConstants
    {
        public static readonly Dictionary<string, string> PrimaryToSecondaryCommandMap = new Dictionary<string, string>()
        {
            { "north", "n" },
            { "northeast", "ne" },
            { "east", "e" },
            { "southeast", "se" },
            { "south", "s" },
            { "southwest", "sw" },
            { "west", "w" },
            { "northwest", "nw" },
            { "up", "u" },
            { "down", "d" },
            { "enter", "en" },
            { "exit", "ex" }
        };

        public static readonly Dictionary<string, string> MirrorDirectionMap = new Dictionary<string, string>()
        {
            { "north", "south" },
            { "northeast", "southwest" },
            { "east", "west" },
            { "southeast", "northwest" },
            { "south", "north" },
            { "southwest", "northeast" },
            { "west", "east" },
            { "northwest", "southeast" },
            { "up", "down" },
            { "down", "up" },
            { "enter", "exit" },
            { "exit", "enter" }
        };
    }
}
