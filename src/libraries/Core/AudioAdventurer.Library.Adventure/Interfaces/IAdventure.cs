using System;
using System.Collections.Generic;

namespace AudioAdventurer.Library.Adventure.Interfaces;

public interface IAdventure
{
    public Guid PublisherId { get; set; }
    public Guid CompatibleUniverseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinRecommendedCharacterLevel { get; set; }
    public int MaxRecommendedCharacterLevel { get; set; }
    public List<string> RequiredGameExtensions { get; set; }
    public Guid InitialThingNodeId { get; set; }
    public List<IThingNode> Things { get; set; }
}