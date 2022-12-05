namespace AudioAdventurer.Library.Common.Constants
{
    public enum VerbalCommunicationType
    {
        /// <summary>Normal speech that can be perceived within the bounds of a room.</summary>
        Say,

        /// <summary>Private communication with a single sender and receiver.</summary>
        Tell,

        /// <summary>Loud communication that can be heard at long distances.</summary>
        Yell,

        /// <summary>A user-defined action perceived to all in the room</summary>
        Emote,
    }
}
