using System.Security.Cryptography;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class RandomHelper
    {
        public static int GetRandomInt(int min, int max)
        {
            return RandomNumberGenerator.GetInt32(min, max);
        }
    }
}
