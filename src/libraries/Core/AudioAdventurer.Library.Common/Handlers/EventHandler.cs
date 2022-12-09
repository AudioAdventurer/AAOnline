using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Delegates;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Models;
using System.Collections.Generic;
using System;

namespace AudioAdventurer.Library.Common.Handlers
{
    /// <summary>
    /// Used by Thing objects to dispatch events
    /// </summary>
    public class EventHandler
    {
        private readonly Thing _owner;

        public EventHandler(Thing owner)
        {
            _owner = owner;
        }

        public event GameEventHandler CombatEvent;

        public event CancellableGameEventHandler CombatRequest;

        public event GameEventHandler MovementEvent;

        public event CancellableGameEventHandler MovementRequest;

        public event GameEventHandler CommunicationEvent;

        public event CancellableGameEventHandler CommunicationRequest;

        public event GameEventHandler MiscellaneousEvent;

        public event CancellableGameEventHandler MiscellaneousRequest;

        public void OnMovementEvent(
            AbstractGameEvent e,
            EventScope eventScope)
        {

        }

        public void OnMovementRequest(
            CancellableGameEvent e,
            EventScope eventScope)
        {
        }

        private void OnEvent(
            Func<EventHandler, GameEventHandler> handlerSelector,
            AbstractGameEvent e,
            EventScope eventScope)
        {

        }

        private void OnRequest(
            Func<EventHandler, CancellableGameEventHandler> handlerSelector,
            CancellableGameEvent e,
            EventScope eventScope)
        {

        }

        private void OnRequest(
            Func<EventHandler, CancellableGameEventHandler> handlerSelector,
            CancellableGameEvent e,
            bool cascadeEventToChildren)
        {
            // Build a request target queue which starts with our owner Thing and visits all it's Children.
            // (This is a queue instead of recursion to help avoid stack overflows and such with very large object trees.)
            Queue<Thing> requestTargetQueue = new Queue<Thing>();
            requestTargetQueue.Enqueue(_owner);

            while (requestTargetQueue.Count > 0)
            {
                // If anything (like one of the thing's Behaviors) is subscribed to this request, send it there.
                Thing currentRequestTarget = requestTargetQueue.Dequeue();
                var handler = handlerSelector(currentRequestTarget.EventHandler);
                if (handler != null)
                {
                    handler(currentRequestTarget, e);

                    // If the event has been canceled by the handler, we no longer need to look for further permission.
                    if (e.IsCanceled)
                    {
                        break;
                    }
                }

                if (cascadeEventToChildren)
                {
                    // Enqueue all the current target's children for processing.
                    // foreach (Thing child in currentRequestTarget.Children)
                    // {
                    //     requestTargetQueue.Enqueue(child);
                    // }
                }
            }
        }
    }
}
