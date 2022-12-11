using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Helpers
{
    public static class ThingHelper
    {
        public static T FindBehavior<T>(this IThing thing) 
            where T : IBehavior
        {
            return thing.BehaviorHandler.FindFirst<T>();
        }

        public static IEnumerable<T> FindBehaviors<T>(this IThing thing)
            where T : IBehavior
        {
            return thing.BehaviorHandler.Find<T>();
        }

        public static bool HasBehavior<T>(this IThing thing)
            where T : IBehavior
        {
            return thing.BehaviorHandler.FindFirst<T>() != null;
        }

        public static List<IThing> GetParents(this IThing thing)
        {
            var parents = new List<IThing>();

            foreach (var guid in thing.Parents)
            {
                var parent = thing.ThingService.GetThing(guid);
                parents.Add(parent);
            }

            return parents;
        }

        public static List<IThing> GetChildren(this IThing thing)
        {
            var children = new List<IThing>();

            foreach (var guid in thing.Children)
            {
                var parent = thing.ThingService.GetThing(guid);
                children.Add(parent);
            }

            return children;
        }

        public static IThing FindParentRoom(this IThing thing)
        {
            if (thing.HasBehavior<RoomBehavior>())
            {
                return thing;
            }

            var parents = thing.GetParents();

            foreach (var parent in parents)
            {
                if (parent.HasBehavior<RoomBehavior>())
                {
                    return parent;
                }

                // Recurse until we find a room
                var room = FindParentRoom(parent);
                if (room != null)
                {
                    return room;
                }
            }

            // No room found
            return null;
        }

        public static bool IsContextualDirectionCommand(this IThing thing, string command)
        {
            // Find the nearest room parent
            IThing room = thing.FindParentRoom();

            if (room != null)
            {
                var children = room.GetChildren();

                foreach (var child in children)
                {
                    // Get the exits
                    var exitBehaviors = child.FindBehaviors<ExitBehavior>();

                    // Go through each exit and see if the commands match the command to test against.
                    foreach (var exitBehavior in exitBehaviors)
                    {
                        foreach (var exitBehaviorDestination in exitBehavior.Destinations)
                        {
                            // Verify the target isn't this room
                            if (!exitBehaviorDestination.TargetId.Equals(room.Id))
                            {
                                // Test if the command passed in is this command
                                if (exitBehaviorDestination.ExitCommand.Equals(command, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    // Exit as soon as we find a match
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            // Didn't find a match
            return false;
        }

        public static IThing FindChild(
            this IThing thing, 
            string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return null;
            }

            lock (thing.Lock)
            {
                foreach (var child in thing.GetChildren())
                {
                    if (child.Name.Equals(
                            searchString, 
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        return child;
                    }

                    if (child.Name.StartsWith(
                            searchString,
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        return child;
                    }
                }
            }

            return null;
        }
    }
}
