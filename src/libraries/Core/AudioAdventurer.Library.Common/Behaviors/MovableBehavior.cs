using System.Collections.Generic;
using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Behaviors
{
    public class MovableBehavior : AbstractBehavior
    {
        private readonly IThingService _thingService;

        public MovableBehavior(
            IBehaviorData behaviorInfo,
            IThingService thingService) 
            : base(behaviorInfo)
        {
            _thingService = thingService;
        }

        public override void SetProperties(
            Dictionary<string, string> behaviorInfo)
        {
            
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }

        public bool Move(
            IThing destination, 
            IThing goingVia, 
            SensoryMessage leavingMessage,
            SensoryMessage arrivingMessage)
        {
            IThing actor = Parent;
            var goingFromId = actor.Parents.FirstOrDefault();
            var goingFrom = _thingService.GetThing(goingFromId);

            // Prepare events to request and send (if not canceled).
            var leaveEvent = new LeaveEvent(actor, leavingMessage)
            {
                GoingFrom = goingFrom,
                GoingTo = destination,
                GoingVia = goingVia
            };

            var arriveEvent = new ArriveEvent(actor, arrivingMessage)
            {
                GoingFrom = goingFrom,
                GoingTo = destination,
                GoingVia = goingVia
            };

            // Broadcast the Leave Request first to see if the player is allowed to leave.
            actor.EventManager.OnMovementRequest(leaveEvent, EventScope.ParentsDown);
            if (!leaveEvent.IsCanceled)
            {
                // Next see if the player is allowed to Arrive at the new location.
                destination.EventManager.OnMovementRequest(arriveEvent, EventScope.SelfDown);
                if (!arriveEvent.IsCanceled)
                {
                    actor.EventManager.OnMovementEvent(leaveEvent, EventScope.ParentsDown);
//                    actor.RemoveFromParents();
                    destination.AddChild(actor);

                    // TODO: Ensure these automatically enqueue a save.
                    destination.EventManager.OnMovementEvent(arriveEvent, EventScope.SelfDown);
                    return true;
                }
            }

            return false;
        }
    }
}
