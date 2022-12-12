using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Models
{
    public class ContextualString
    {
        public ContextualString(
            IThing originator,
            IThing receiver,
            string rawMessage)
        {
            Originator = originator;
            Receiver = receiver;
            RawMessage = rawMessage;
        }

        public IThing Originator { get; }
        public IThing Receiver { get; }

        public string RawMessage { get; set; }

        public string ToOriginator { get; set; }
        public string ToReceiver { get; set; }
        public string ToOthers { get; set; }
    }
}