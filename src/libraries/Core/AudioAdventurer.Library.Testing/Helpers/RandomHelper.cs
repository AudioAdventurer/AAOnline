using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AudioAdventurer.Library.Testing.Helpers
{
    public static class RandomHelper
    {
        private static readonly string[] Characters =
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
            "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "1", "2", "3", "4", "5", "6", "7", "8",
            "9", "0", "!", "-"
        };

        public static string GetRandomString(int _length)
        {
            StringBuilder sb = new StringBuilder();

            Byte[] array = GetRandomArray(_length);

            for (int i = 0; i < _length; i++)
            {
                int b = array[i] % 64;

                sb.Append(Characters[b]);
            }

            return sb.ToString();
        }

        private static byte[] GetRandomArray(int _length)
        {
            var array = RandomNumberGenerator.GetBytes(_length);
            return array;
        }

        public static int GetRandomInt(int min, int max)
        {
            return RandomNumberGenerator.GetInt32(min, max);
        }

        public static double GetRandomDouble(int min, int max)
        {
            var random = new Random();

            return random.NextDouble() * (max - min) + min;
        }
    }
}
