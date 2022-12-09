namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IServerOutputEntry
    {
        public string TextType { get; }

        public string Text { get; }

        public bool AppendLine { get; }
    }
}
