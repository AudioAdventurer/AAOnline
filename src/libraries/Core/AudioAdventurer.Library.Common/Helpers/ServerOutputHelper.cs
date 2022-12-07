using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ServerOutputHelper
    {
        public static void AppendEntry(
            this IServerOutput output,
            string type,
            string text)
        {
            var entry = new ServerOutputEntry(type, text);
            output.Entries.Add(entry);
        }

        public static IServerOutput GetSimpleOutput(string text)
        {
            var serverOutput = new ServerOutput();
            serverOutput.AppendEntry("text", text);
            return serverOutput;
        }
    }
}
