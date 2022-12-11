using System;
using AudioAdventurer.Library.Common.EventArguments;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Sessions
{
    public class Session : ISession
    {
        public Session(IThing player)
        {
            Player = player;
        }

        public IThing Player { get; }

        public event EventHandler UserInputReceived;
        public event EventHandler ServerOutputReceived;
        
        public void WriteServerOutput(
            IServerOutput output)
        {
            var args = new ServerOutputReceivedEventArgs()
            {
                ServerOutput = output,
                Session = this
            };

            OnServerOutputReceived(args);
        }

        public void ProcessUserInput(string command)
        {
            var args = new UserInputReceivedEventArgs
            {
                Command = command,
                Session = this,
                Actor = Player
            };

            OnUserInputReceived(args);
        }

        protected virtual void OnUserInputReceived(
            UserInputReceivedEventArgs e)
        {
            UserInputReceived?.Invoke(this, e);
        }

        protected virtual void OnServerOutputReceived(
            ServerOutputReceivedEventArgs e)
        {
            ServerOutputReceived?.Invoke(this, e);
        }
    }
}
