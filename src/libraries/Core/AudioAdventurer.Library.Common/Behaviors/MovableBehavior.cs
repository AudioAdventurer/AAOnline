using System.Linq;
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

            goingFrom.RemoveChild(actor);
            destination.AddChild(actor);


            return true;
        }
    }
}
