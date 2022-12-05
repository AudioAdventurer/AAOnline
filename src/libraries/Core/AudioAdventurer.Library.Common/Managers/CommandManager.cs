﻿using System.Threading;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Managers
{
    public class CommandManager : ICommandManager
    {
        private readonly IThingService _thingService;
        private bool _running;
        private Thread _thread;
        private IGameManager _gameManager;

        public CommandManager(
            IThingService thingService)
        {
            _thingService = thingService;
        }

        public bool Running => _running;

        public void Start(IGameManager gameManager)
        {
            _gameManager = gameManager;

            if (!_running)
            {
                _running = true;
                _gameManager.ActionEnqueued += ActionEnqueued;

                _thread = new Thread(Run);
                _thread.Start();
            }
        }

        private void ActionEnqueued(object sender, System.EventArgs e)
        {
            var action = _gameManager.DequeueAction();

            //TODO Action processing
            action?.Session?.WriteServerOutput($"You {action.FullText}");
        }

        public void Stop()
        {
            if (_running)
            {
                _running = false;
            }
        }

        private void Run()
        {
            do
            {
                Thread.Sleep(100);
            } while (_running);

            _gameManager.ActionEnqueued -= ActionEnqueued;
        }
    }
}