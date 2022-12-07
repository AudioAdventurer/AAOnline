﻿using System.Threading;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Managers
{
    public class CommandManager : ICommandManager
    {
        private readonly IActionManager _actionManager;
        private bool _running;
        private Thread _thread;
        private IGameManager _gameManager;

        public CommandManager(
            IActionManager actionManager)
        {
            _actionManager = actionManager;
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

            if (action != null)
            {
                _actionManager.HandleAction(action);
            }
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
