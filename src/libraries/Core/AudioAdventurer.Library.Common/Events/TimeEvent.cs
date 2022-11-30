﻿using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.Library.Common.Events
{
    public class TimeEvent 
        : CancellableGameEvent
    {
        public TimeEvent(
            Thing activeThing, 
            SensoryMessage sensoryMessage) 
            : base(activeThing, sensoryMessage)
        {
        }
    }
}
