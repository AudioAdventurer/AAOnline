using AudioAdventurer.Library.Common.Constants;
using System.Collections;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Senses
{
    public class SensoryMessage
    {
        public SensoryMessage(
            SensoryType targetedSense,
            int messageStrength,
            ContextualString message)
        {
            TargetedSense = targetedSense;
            MessageStrength = messageStrength;
            Message = message;
        }

        public SensoryType TargetedSense { get; private set; }

        /// <summary>Gets the strength of the message.</summary>
        public int MessageStrength { get; private set; }

        /// <summary>Gets the raw message to be processed by sense receptors.</summary>
        public ContextualString Message { get; private set; }
    }
}
