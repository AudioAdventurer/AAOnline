namespace AudioAdventurer.Library.Common.Constants
{
    public enum CommonGuards
    {
        /// <summary>The initiator of the action must be alive.</summary>
        InitiatorMustBeAlive,

        /// <summary>The initiator of the action must be conscious.</summary>
        InitiatorMustBeConscious,

        /// <summary>The initiator of the action must be standing.</summary>
        InitiatorMustBeStanding,

        /// <summary>The initiator of the action must be balanced.</summary>
        /// <remarks>Not Implemented. Need new implementation not based on stats.</remarks>
        InitiatorMustBeBalanced,

        /// <summary>The initiator of the action must be able to move.</summary>
        /// <remarks>Not Implemented. Need new implementation not based on stats.</remarks>
        InitiatorMustBeMobile,

        /// <summary>The initiator of the action must be a player (and have a known player Session).</summary>
        InitiatorMustBeAPlayer,

        /// <summary>There must be at least one additional argument.</summary>
        RequiresAtLeastOneArgument,

        /// <summary>There must be at least two additional arguments.</summary>
        RequiresAtLeastTwoArguments
    }
}
