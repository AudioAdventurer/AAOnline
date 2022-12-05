namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IThingData : IIdentifiableObject
    {
        string Name { get; set; }

        string Description { get; set; }

        string Title { get; set; }

        public string SingularPrefix { get; set; }

        public string PluralSuffix { get; set; }

        public int MaxChildren { get; set; }

        public int MaxParents { get; set; }
    }
}