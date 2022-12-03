
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ThingHelper
    {
        public static T FindBehavior<T>(this IThing thing) 
            where T : IBehavior
        {
            return thing.BehaviorManager.FindFirst<T>();
        }


        public static bool HasBehavior<T>(this IThing thing)
            where T : IBehavior
        {
            return thing.BehaviorManager.FindFirst<T>() != null;
        }

    }
}
