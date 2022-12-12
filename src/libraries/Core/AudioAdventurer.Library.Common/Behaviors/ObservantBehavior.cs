using System;
using AudioAdventurer.Library.Common.EventArguments;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Behaviors
{
    /// <summary>
    /// Uses senses to observer the information in the world
    /// </summary>
    public class ObservantBehavior 
        : AbstractBehavior
    {
        private readonly IThingService _thingService;
        private readonly IMessageBus _messageBus;

        public ObservantBehavior(
            IBehaviorData behaviorInfo,
            IThingService thingService) 
            : base(behaviorInfo)
        {
            _thingService = thingService;
            _messageBus = thingService.GetMessageBus();
            _messageBus.MessageReceived += MessageReceived;
        }

        private void MessageReceived(object sender, EventArgs e)
        {
            if (e is MessageReceivedEventArgs args)
            {
                var parent = Parent;

                var gameEvent = args.Message.Event;

                var behaviors = parent.BehaviorHandler.AllBehaviors;

                foreach (var behavior in behaviors)
                {
                    if (behavior is IResponsiveBehavior responsiveBehavior)
                    {
                        responsiveBehavior.RespondAsRequired(gameEvent);
                    }
                }
            }
        }

        public override IBehaviorData GetProperties()
        {
            return _behaviorData;
        }
    }
}
