using System.IO;

namespace AudioAdventurer.Library.Data.Helpers;

public static class DirectoryHelper
{
    public static void EnsureDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}