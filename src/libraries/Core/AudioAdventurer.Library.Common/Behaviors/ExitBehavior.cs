﻿using System;
using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class ExitBehavior
        : AbstractBehavior
    {
        private readonly List<ExitDestination> _destinations;
        private readonly IThingService _thingService;

        public ExitBehavior(
            IBehaviorData behaviorInfo, IThingService thingService)
            : base(behaviorInfo)
        {
            _destinations = new List<ExitDestination>();
            _thingService = thingService;
        }

        public override void SetProperties(
            Dictionary<string, string> properties)
        {
            lock (_lock)
            {
                _destinations.Clear();

                if (properties.ContainsKey("destinations"))
                {
                    string json = properties["destinations"];
                    var temp = JsonHelper.Deserialize<List<ExitDestination>>(json);
                    _destinations.AddRange(temp);
                }
            }
        }

        public override IBehaviorData GetProperties()
        {
            lock (_lock)
            {
                var behaviorData = _behaviorData;
                behaviorData.Properties.Clear();

                behaviorData.Properties.Add(
                    "destinations",
                    JsonHelper.Serialize(_destinations));                

                return behaviorData;
            }
        }

        public void AddDestination(
            string movementCommand,
            Guid destinationId)
        {
            movementCommand = DirectionHelper.NormalizeDirection(movementCommand);

            lock (_lock)
            {
                ExitDestination existing = null;
                foreach (var existingDestination in _destinations)
                {
                    if (existingDestination
                        .ExitCommand.Equals(
                            movementCommand,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        existing = existingDestination;
                        break;
                    }
                }

                if (existing != null)
                {
                    _destinations.Remove(existing);
                }

                if (_destinations.Count < 2)
                {
                    _destinations.Add(new ExitDestination()
                    {
                        ExitCommand = movementCommand,
                        TargetId = destinationId
                    });
                }
            }
        }

        public bool MoveThrough(Thing thingToMove)
        {
            // If the thing isn't currently mobile, bail.
            var movableBehavior = thingToMove.FindBehavior<MovableBehavior>();
            if (movableBehavior == null)
            {
                // TODO: Add messaging to thingToMove?
                return false;
            }

            var parent = thingToMove.Parents.FirstOrDefault();
            if (parent == null)
            {
                return false;
            }

            // Find the target location to be reached from here.
            var destinationInfo = _destinations.GetDestinationFrom(parent.Info.Id);
            if (destinationInfo == null)
            {
                // There was no destination reachable from the thing's starting location.
                return false;
            }

            // If the destination can't be found, abort.
            IThing destination = _thingService.GetThing(destinationInfo.TargetId);
            if (destination == null)
            {
                // TODO: Add messaging to thingToMove?
                return false;
            }

            string dir = destinationInfo.ExitCommand;

            var leaveContextMessage = new ContextualString(thingToMove, parent)
            {
                ToOriginator = null,
                ToReceiver = $"{thingToMove.Info.Name} moves {dir}.",
                ToOthers = $"{thingToMove.Info.Name} moves {dir}.",
            };

            var arriveContextMessage = new ContextualString(thingToMove, destination)
            {
                ToOriginator = $"You move {dir} to {destination.Info.Name}.",
                ToReceiver = $"{thingToMove.Info.Name} arrives, heading {dir}.",
                ToOthers = $"{thingToMove.Info.Name} arrives, heading {dir}.",
            };
            var leaveMessage = new SensoryMessage(SensoryType.Sight, 100, leaveContextMessage);
            var arriveMessage = new SensoryMessage(SensoryType.Sight, 100, arriveContextMessage);

            return movableBehavior.Move(destination, Parent, leaveMessage, arriveMessage);
        }
    }
}
