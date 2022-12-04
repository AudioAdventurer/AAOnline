using System;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface ISession
{
    public IThing Player { get; set; }

    public event EventHandler UserInputReceived;
    public event EventHandler ServerOutputReceived;

    public void WriteServerOutput(
        string singleLineOutput);

    public void ProcessUserInput(
        string command);
}