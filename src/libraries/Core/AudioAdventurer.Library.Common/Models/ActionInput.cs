using AudioAdventurer.Library.Common.Sessions;
using System.Linq;
using System;

namespace AudioAdventurer.Library.Common.Models
{
    public class ActionInput
    {
        public ActionInput(
            string fullText,
            Session session,
            Thing actor)
        {
            Session = session;
            Actor = actor;
            FullText = fullText;

            ParseText(fullText);
        }

        public string FullText { get; private set; }

        public string Noun { get; private set; }

        public string Tail { get; private set; }

        public Session Session { get; private set; }

        public Thing Actor { get; private set; }

        public string[] Params { get; private set; }

        private void ParseText(string fullText)
        {
            fullText = string.IsNullOrEmpty(fullText)
                ? string.Empty : fullText.Trim();

            string[] words = fullText
                .Split(new[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0)
            {
                Noun = words[0].ToLower();
                Tail = fullText.Remove(0, Noun.Length).Trim();
                Params = words.Skip(1).ToArray();
            }
        }
    }
}
