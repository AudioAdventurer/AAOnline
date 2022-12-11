namespace AudioAdventurer.Library.Common.Interfaces;

public interface IActionInput
{
    public string FullText { get; }

    public string Action { get; }

    public string Tail { get; }

    public ISession Session { get; }

    public IThing Actor { get; }

    public string[] Params { get; }
}