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

            Context = new Hashtable();
        }

        public Hashtable Context { get; }

        public SensoryType TargetedSense { get; }

        /// <summary>Gets the strength of the message.</summary>
        public int MessageStrength { get; }

        /// <summary>Gets the raw message to be processed by sense receptors.</summary>
        public ContextualString Message { get; }
    }
}
