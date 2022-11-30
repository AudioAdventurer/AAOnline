using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Behaviors;
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

        public static IEnumerable<T> FindAllChildrenBehaviors<T>(
            this IThing thing)
            where T : IBehavior
        {
            return from child in thing.Children
                let behavior = child.FindBehavior<T>()
                select behavior;
        }

        public static bool HasBehavior<T>(this IThing thing)
            where T : IBehavior
        {
            return thing.BehaviorManager.FindFirst<T>() != null;
        }
    }
}
