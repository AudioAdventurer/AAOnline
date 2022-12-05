using System;
using System.Threading;
using AudioAdventurer.Library.Common.EventArguments;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Client.Local.Manager
{
    public class ClientManager
        : IClientManager
    {
        private readonly ISession _session;
        private Thread _thread;
        private bool _running;

        public ClientManager(
            ISession session)
        {
            _session = session;
            _session.ServerOutputReceived += ServerOutputReceived;

        }

        public void Start()
        {
            if (!_running)
            {
                _running = true;

                _thread = new Thread(Run);
                _thread.Start();
            }
        }

        public void Stop()
        {
            if (_running)
            {
                _running = false;
            }
        }

        // Blocking
        public void Run()
        {
            do
            {
                var command = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(command))
                {
                    _session.ProcessUserInput(command);
                }

                Thread.Sleep(250);
            } while (_running);
        }

        private void ServerOutputReceived(object? sender, EventArgs e)
        {
            if (e is ServerOutputReceivedEventArgs args)
            {
                Console.WriteLine(args.ServerOutput);
            }
        }
    }
}
