using System;
using System.Linq;
using System.Text;
using AudioAdventurer.Library.Common.Interfaces;

namespace AudioAdventurer.Library.Client.Local.Renderers
{
    public class ConsoleServerOutputRenderer
        : IServerOutputRenderer
    {
        public void RenderPrompt()
        {
            Console.Write("> ");
        }

        public void Render(IServerOutput output)
        {
            if (output != null
                && output.Entries.Any())
            {
                //TODO - Take type into account.
                StringBuilder sb = new StringBuilder();

                foreach (var entry in output.Entries)
                {
                    if (entry.AppendLine)
                    {
                        sb.AppendLine(entry.Text);
                    }
                    else
                    {
                        sb.Append(entry.Text);
                        sb.Append(" ");
                    }
                }

                var text = sb.ToString().TrimEnd();

                if (text.EndsWith("."))
                {
                    Console.WriteLine(text);
                }
                else
                {
                    Console.Write(text);
                    Console.WriteLine(".");
                }
            }

            RenderPrompt();
        }
    }
}
