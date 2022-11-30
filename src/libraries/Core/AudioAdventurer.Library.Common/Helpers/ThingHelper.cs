using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ThingHelper
    {
        public static T FindBehavior<T>(this IThing thing) 
            where T : AbstractBehavior
        {
            return thing.BehaviorManager.FindFirst<T>();
        }

        public static IEnumerable<T> FindAllChildrenBehaviors<T>(this IThing thing)
            where T : AbstractBehavior
        {
            return from child in thing.Children
                let behavior = child.FindBehavior<T>()
                select behavior;
        }
    }
}
