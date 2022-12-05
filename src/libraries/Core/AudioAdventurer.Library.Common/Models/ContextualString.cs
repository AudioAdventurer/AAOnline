using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ContextualString
    {
        public ContextualString(
            IThing originator,
            IThing receiver)
        {
            Originator = originator;
            Receiver = receiver;
        }

        public IThing Originator { get; }
        public IThing Receiver { get; }

        public string ToOriginator { get; set; }
        public string ToReceiver { get; set; }
        public string ToOthers { get; set; }
    }
}