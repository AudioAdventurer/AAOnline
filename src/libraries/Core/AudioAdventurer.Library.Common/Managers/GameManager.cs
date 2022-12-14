using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using AudioAdventurer.Library.Common.EventArguments;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Models;

namespace AudioAdventurer.Library.Common.Managers
{
    /// <summary>
    /// The Game Manager controls the overall adventure
    /// </summary>
    public class GameManager : IGameManager
    {
        private readonly object _lock;
        private readonly List<ISession> _sessions;
        private readonly Queue<IActionInput> _actions;
        private readonly ICommandManager _commandManager;
        private readonly IMessageBus _messageBus;

        private bool _running;
        private bool _stopping;
        private Thread _runThread;

        public event EventHandler GameManagerStarted;
        public event EventHandler GameManagerStopped;
        public event EventHandler SessionAdded;
        public event EventHandler SessionRemoved;
        public event EventHandler ActionEnqueued;
        public event EventHandler ActionDequeued;

        public GameManager(
            ICommandManager commandManager,
            IMessageBus messageBus)
        {
            _lock = new object();
            _commandManager = commandManager;

            _sessions = new List<ISession>();
            _actions = new Queue<IActionInput>();

            messageBus.MessageReceived += MessageBus_MessageReceived;
            _messageBus = messageBus;

        }

        private void MessageBus_MessageReceived(object sender, EventArgs e)
        {
            if (e is MessageReceivedEventArgs args)
            {
                var ge = args.Message.Event;

                if (ge is SubmitCommandEvent sce)
                {
                    var actionInput = new ActionInput(
                        sce.CommandText,
                        null, 
                        sce.Actor);

                    Task.Run(() =>
                    {
                        EnqueueAction(actionInput);
                    });
                }
            }
        }

        public void Start()
        {
            if (!_running)
            {
                lock (_lock)
                {
                    _runThread = new Thread(Run);
                    _runThread.Start();

                    _running = true;
                }
            }
        }

        public void Stop()
        {
            if (_running)
            {
                lock (_lock)
                {
                    _stopping = true;
                }
            }
        }

        public bool Running => _running;

        private void Run()
        {
            OnStartHandler();

            _commandManager.Start(this);
            
            do
            {
                Thread.Sleep(250);
            } while (_running && !_stopping);

            if (_actions.Count > 0)
            {
                // Give system a chance to empty queue
                var waitTime = DateTime.UtcNow.AddSeconds(5);

                do
                {
                    Thread.Sleep(250);
                } while (DateTime.UtcNow < waitTime);
            }

            _commandManager.Stop();
            
            OnStopHandler();

            _running = false;
            _stopping = false;
        }

        public bool AddSession(ISession session)
        {
            lock (_lock)
            {
                if (!_running || _stopping)
                {
                    return false;
                }

                if (!_sessions.Contains(session))
                {
                    session.UserInputReceived += UserInputReceived;
                    _sessions.Add(session);

                    Task.Run(() =>
                    {
                        OnSessionAddedHandler(session);
                    });
                }

                return true;
            }
        }

        private void UserInputReceived(object sender, EventArgs e)
        {
            if (e is UserInputReceivedEventArgs args)
            {
                var command = args.Command;
                var session = args.Session;
                var actor = args.Actor;

                // rather than forcing "move east"
                // allow user to just say "east" and 
                // then add "move" to the command here.
                // This should only be use for simple movement.
                // If further contextual commands arise, then a more generic
                // behavior function like IsContextualCommand(command, out string commandType) 
                // may be needed.
                if (args.Actor.IsContextualDirectionCommand(command))
                {
                    //Ok so this is a contextual move command
                    command = $"move {command.Trim()}";
                }

                var action = new ActionInput(
                    command,
                    session,
                    actor);

                Task.Run(() =>
                {
                    EnqueueAction(action);
                });
            }
        }

        public void RemoveSession(ISession session)
        {
            lock (_lock)
            {
                if (_sessions.Contains(session))
                {
                    session.UserInputReceived -= UserInputReceived;
                    _sessions.Remove(session);

                    Task.Run(() =>
                    {
                        OnSessionRemovedHandler(session);
                    });
                }
            }
        }

        public bool EnqueueAction(IActionInput action)
        {
            lock (_lock)
            {
                if (!_running || _stopping)
                {
                    return false;
                }

                _actions.Enqueue(action);

                Task.Run(() =>
                {
                    OnActionEnqueuedHandler(action);
                });

                return true;
            }
        }

        public IActionInput DequeueAction()
        {
            lock (_lock)
            {
                if (_actions.TryDequeue(out IActionInput action))
                {
                    Task.Run(() =>
                    {
                        OnActionDequeuedHandler(action);
                    });
                    
                    return action;
                }

                return null;
            }
        }

        private void OnStartHandler()
        {
            GameManagerStarted?.Invoke(this, EventArgs.Empty);
        }

        private void OnStopHandler()
        {
            GameManagerStopped?.Invoke(this, EventArgs.Empty);
        }

        private void OnSessionAddedHandler(ISession session)
        {
            SessionAddedEventArgs args = new SessionAddedEventArgs
            {
                Session = session
            };

            SessionAdded?.Invoke(this, args);
        }

        private void OnSessionRemovedHandler(ISession session)
        {
            SessionRemovedEventArgs args = new SessionRemovedEventArgs
            {
                Session = session
            };

            SessionRemoved?.Invoke(this, args);
        }

        private void OnActionEnqueuedHandler(IActionInput action)
        {
            ActionEnqueuedEventArgs args = new ActionEnqueuedEventArgs
            {
                Action = action
            };

            ActionEnqueued?.Invoke(this, args);
        }

        private void OnActionDequeuedHandler(IActionInput action)
        {
            ActionDequeuedEventArgs args = new ActionDequeuedEventArgs
            {
                Action = action
            };

            ActionDequeued?.Invoke(this, args);
        }
    }
}
