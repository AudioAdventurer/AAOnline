using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ServerOutputEntry : IServerOutputEntry
    {
        public ServerOutputEntry(
            string textType, 
            string text,
            bool appendLine)
        {
            TextType = textType;
            Text = text;
            AppendLine = appendLine;
        }

        public string TextType { get; }
        public string Text { get; }
        public bool AppendLine { get; }
    }
}
