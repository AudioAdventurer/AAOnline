using AudioAdventurer.Library.Common.Sessions;

namespace AudioAdventurer.Library.Common.Interfaces;

public interface IRequiresSession
{
    public ISession Session { get; set; }
}