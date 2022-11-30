using System.Collections;

namespace AudioAdventurer.Library.Common.Senses
{
    public class SensoryMessage
    {
        public SensoryMessage()
        {
            Context = new Hashtable();
        }

        public Hashtable Context { get; private set; }
    }
}
