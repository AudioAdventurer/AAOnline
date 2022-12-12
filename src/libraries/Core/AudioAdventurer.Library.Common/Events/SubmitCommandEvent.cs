using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Events
{
    public class SubmitCommandEvent : IGameEvent
    {
        public IThing Actor { get; set; }
        public string CommandText { get; set; }
    }
}
