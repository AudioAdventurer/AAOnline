using AudioAdventurer.Library.Common.Constants;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Models
{
    public class GameStat : AbstractBaseObject
    {
        private int _currentValue;
        private int _minValue;
        private int _maxValue;

        public string Name { get; set; }

        public string Abbreviation { get; private set; }

        public string Formula { get; set; }

        public bool Visible { get; private set; }

        public int Value
        {
            get
            {
                lock (Lock)
                {
                    return _currentValue;
                }
            }
        }

        public int MinValue
        {
            get
            {
                lock (Lock)
                {
                    return _minValue;
                }
            }

            set
            {
                lock (Lock)
                {
                    _minValue = value;
                }
            }
        }

        public int MaxValue
        {
            get
            {
                lock (Lock)
                {
                    return _maxValue;
                }
            }

            set
            {
                lock (Lock)
                {
                    _maxValue = value;
                }
            }
        }

    }
}
