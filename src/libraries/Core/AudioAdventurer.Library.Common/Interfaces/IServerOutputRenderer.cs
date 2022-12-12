namespace AudioAdventurer.Library.Common.Interfaces
{
    public interface IServerOutputRenderer
    {
        public void RenderPrompt();

        public void Render(IServerOutput output);
    }
}
