using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ServerOutputHelper
    {
        public static void AppendEntry(
            this IServerOutput output,
            string textType,
            string text,
            bool appendLine)
        {
            var entry = new ServerOutputEntry(
                textType, 
                text, 
                appendLine);

            output.Entries.Add(entry);
        }

        public static IServerOutput AppendEntry(
            this IServerOutput output,
            string text,
            bool appendLine)
        {
            output.AppendEntry(
                ServerOutputDataTypes.Text, 
                text, 
                appendLine);

            return output;
        }
    }
}
