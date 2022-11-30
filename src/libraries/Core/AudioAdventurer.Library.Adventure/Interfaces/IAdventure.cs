using System.Collections.Generic;

namespace AudioAdventurer.Library.Adventure.Interfaces;

public interface IAdventure
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MinRecommendedCharacterLevel { get; set; }
    public int MaxRecommendedCharacterLevel { get; set; }
    public List<string> RequiredGameExtensions { get; set; }
    public List<IThingNode> Things { get; set; }
}