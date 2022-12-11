using System.Text;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Common.Renderers
{
    public class JsonServerOutputRenderer
        : IServerOutputRenderer
    {
        private readonly object _lock;
        private readonly StringBuilder _stringBuilder;

        public JsonServerOutputRenderer(
            StringBuilder stringBuilder)
        {
            _lock = new object();
            _stringBuilder = stringBuilder;
        }

        public void RenderPrompt()
        {
            // Does nothing
        }

        public void Render(IServerOutput output)
        {
            lock (_lock)
            {
                var json = JsonHelper.Serialize(output, true);
                _stringBuilder.Append(json);
            }
        }

        public string Read()
        {
            lock (_lock)
            {
                var output = _stringBuilder.ToString();
                _stringBuilder.Clear();
                return output;
            }
        }
    }
}
