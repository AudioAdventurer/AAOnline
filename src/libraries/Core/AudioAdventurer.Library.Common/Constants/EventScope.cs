namespace AudioAdventurer.Library.Common.Constants
{
    public enum EventScope
    {
        /// <summary>The event is to be broadcast from the current thing's parent(s) and down through all children.</summary>
        ParentsDown,

        /// <summary>The event is to be broadcast from the current thing and down through all children.</summary>
        SelfDown,

        /// <summary>The event is to be broadcast for the current thing only.</summary>
        /// <remarks>For example, if a player tries to look/glance, the player's sub-things do not necessarily need to know.</remarks>
        SelfOnly,
    }
}
