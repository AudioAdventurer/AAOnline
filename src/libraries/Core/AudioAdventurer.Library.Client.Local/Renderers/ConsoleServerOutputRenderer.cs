using System;
using System.Text;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Client.Local.Renderers
{
    public class ConsoleServerOutputRenderer
        : IServerOutputRenderer
    {
        public void Render(IServerOutput output)
        {
            //TODO - Take type into account.
            StringBuilder sb = new StringBuilder();

            foreach (var entry in output.Entries)
            {
                sb.Append(entry.Text);
                sb.Append(" ");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
