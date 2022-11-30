namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThingInfo : IIdentifiableObject
    {
        string Name { get; }

        string FullName { get; set; }

        string Description { get; }

        string Title { get; }
    }
}