using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Constants;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class DirectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string NormalizeDirection(string direction)
        {
            var kvps = DirectionConstants
                .PrimaryToSecondaryCommandMap;

            string found = null;

            foreach (KeyValuePair<string, string> kvp in kvps)
            {
                if (kvp.Value.Equals(direction, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = kvp.Key;
                    break;
                }
            }

            if (found == null)
            {
                return direction;
            }

            return found;
        }
    }
}
