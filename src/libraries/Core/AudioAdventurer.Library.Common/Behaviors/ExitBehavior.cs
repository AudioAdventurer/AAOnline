using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class ExitBehavior
        : AbstractBehavior
    {

        private readonly List<ExitDestination> _destinations;

        public ExitBehavior(IBehaviorInfo behaviorInfo)
            : base(behaviorInfo)
        {
            _destinations = new List<ExitDestination>();
        }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetProperties(IBehaviorInfo behaviorInfo)
        {
            throw new NotImplementedException();
        }

        public override IBehaviorInfo GetProperties()
        {
            throw new NotImplementedException();
        }

        public void AddDestination(
            string movementCommand,
            Thing destination)
        {
            movementCommand = DirectionHelper.NormalizeDirection(movementCommand);

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
                    
                });
            }
        }
    }
}
