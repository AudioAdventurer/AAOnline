﻿namespace AudioAdventurer.Library.Common.Interfaces;

public interface IBehaviorResolver
{
    public IBehavior ResolveBehavior(IBehaviorInfo behaviorInfo);   
}