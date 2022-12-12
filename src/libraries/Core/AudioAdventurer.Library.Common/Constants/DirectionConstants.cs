using System.Collections.Generic;

namespace AudioAdventurer.Library.Common.Constants
{
    public static class DirectionConstants
    {
        public const string North = "north";
        public const string NorthEast = "northeast";
        public const string East = "east";
        public const string SouthEast = "southeast";
        public const string South = "south";
        public const string SouthWest = "southwest";
        public const string West = "west";
        public const string NorthWest = "northwest";
        public const string Up = "up";
        public const string Down = "down";
        public const string Enter = "enter";
        public const string Exit = "exit";

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
