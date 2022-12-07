using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ServerOutputEntry : IServerOutputEntry
    {
        public ServerOutputEntry(
            string type, 
            string text)
        {
            Type = type;
            Text = text;
        }

        public string Type { get; }
        public string Text { get; }
    }
}
