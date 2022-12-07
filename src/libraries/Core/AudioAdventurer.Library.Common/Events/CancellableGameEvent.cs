using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class CancellableGameEvent 
        : AbstractGameEvent
    {
        private bool _sentCancelMessage;

        public CancellableGameEvent(
            IThing activeThing, SensoryMessage sensoryMessage)
            : base(activeThing, sensoryMessage)
        {
            _sentCancelMessage = false;
        }

        public bool IsCanceled { get; private set; }

        public void Cancel(string cancelMessage)
        {
            IsCanceled = true;
            if (!string.IsNullOrEmpty(cancelMessage) 
                && !_sentCancelMessage)
            {
                var output = ServerOutputHelper.GetSimpleOutput(cancelMessage);

                // Write up to one cancellation message directly to the user/initiator if appropriate.
                var userControlledBehavior = ActiveThing.FindBehavior<UserControlledBehavior>();
                userControlledBehavior?
                    .Session?
                    .WriteServerOutput(output);

                _sentCancelMessage = true;
            }
        }
    }
}
