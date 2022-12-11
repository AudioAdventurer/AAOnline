using System.Linq;
using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
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
            IThing actor = _thingService.GetThing(ParentId);
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
            actor.EventHandler.OnMovementRequest(leaveEvent, EventScope.ParentsDown);
            if (!leaveEvent.IsCanceled)
            {
                // Next see if the player is allowed to Arrive at the new location.
                destination.EventHandler.OnMovementRequest(
                    arriveEvent, 
                    EventScope.SelfDown);

                if (!arriveEvent.IsCanceled)
                {
                    actor.EventHandler.OnMovementEvent(
                        leaveEvent, 
                        EventScope.ParentsDown);

                    goingFrom.RemoveChild(actor);
                    destination.AddChild(actor);

                    // TODO: Ensure these automatically enqueue a save.
                    destination.EventHandler.OnMovementEvent(arriveEvent, EventScope.SelfDown);
                    return true;
                }
            }

            return false;
        }
    }
}
