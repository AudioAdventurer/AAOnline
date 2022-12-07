using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface ISession
{
    public IThing Player { get; }

    public event EventHandler UserInputReceived;
    public event EventHandler ServerOutputReceived;

    public void WriteServerOutput(
        IServerOutput output);

    public void ProcessUserInput(
        string command);
}