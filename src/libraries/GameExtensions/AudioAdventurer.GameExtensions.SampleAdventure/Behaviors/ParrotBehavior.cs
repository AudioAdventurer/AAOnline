using System;
using System.Threading;
using AudioAdventurer.Library.Common.Behaviors;
using AudioAdventurer.Library.Common.Events;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Interfaces;
using AudioAdventurer.Library.Common.Senses;

namespace AudioAdventurer.GameExtensions.SampleAdventure.Behaviors
{
    public class ParrotBehavior 
        : AbstractBehavior,
        IResponsiveBehavior
    {
        private int _questionCount;

        public ParrotBehavior(
            IBehaviorData behaviorInfo) 
            : base(behaviorInfo)
        {
            ParseBehavior();
        }

        private void ParseBehavior()
        {
            if (_behaviorData.Properties.ContainsKey("question_count"))
            {
                Int32.TryParse(
                    _behaviorData.Properties["question_count"], 
                    out _questionCount);
            }
        }

        public override IBehaviorData GetProperties()
        {
            _behaviorData.Properties.Clear();
            _behaviorData.Properties.Add("question_count", _questionCount.ToString());

            return _behaviorData;
        }

        public void RespondAsRequired(IGameEvent gameEvent)
        {
           var parrot = Parent;

            if (gameEvent is VerbalCommunicationEvent verbalEvent)
            {
                if (parrot.IsSameRoomDifferentThing(verbalEvent))
                {
                    HandleSensoryMessage(verbalEvent.SensoryMessage);
                }
            }
        }

        private void HandleSensoryMessage(SensoryMessage sensoryMessage)
        {
            var text = sensoryMessage.Message.RawMessage;
            string output = null;

            if (text != null
                && text.Contains("treasure", StringComparison.InvariantCultureIgnoreCase)
                && text.Contains("?"))
            {
                _questionCount++;
                output = AnswerQuestion(_questionCount);
            }
            else
            {
                if (_questionCount < 3)
                {
                    output = GetRudeMessage();
                }
                else
                {
                    // No further response
                }
            }

            if (output != null)
            {
                var parent = Parent;

                parent.EventHandler.SendCommandMessage($"say {output}", parent);
            }
        }

        private string AnswerQuestion(int questionNumber)
        {
            switch (questionNumber)
            {
                case 1:
                    return "You must take the ring through Khazad-dum and then on to Gondor....  Ha ha ha ha.  Had you going there.";
                case 2:
                    return "One does not simply walk to my masters treasure...";
                case 3:
                    return "You must seek the tree that is not a tree, and find the door that is hidden there.";
            }

            return null;
        }

        private string GetRudeMessage()
        {
            var insultId = RandomHelper.GetRandomInt(1, 10);

            switch (insultId)
            {
                case 1:
                    return "Your mother was a hampster and your father smelled of elder berries";
                case 2:
                    return "Away, you scullion! You rampallion! You fustilarian! I'll tickle your catastrophe";
                case 3:
                    return "We'll keel-haul ye, ye lily-livered blowfish... Arrrrgh!";
                case 4:
                    return "You scrawny, dirt-licking, troll-kisser!";
                case 5:
                    return "Your stupidity would give an illithid indigestion.";
                case 6:
                    return "You hideous dirt-licking worm.";
                case 7:
                    return "You loathsome, orc-skinned, daffodil.";
                case 8:
                    return "You weak-minded, snake-faced, flat-foot.";
                case 9:
                    return "Thy paunchy ill-breeding nut-hook hath a hell-hated barnacle.";
                case 10:
                    return "Yarr! I be a pirate, ye pox-faced blowfish... Surrender!.\r\n";
            }

            return null;
        }
    }
}
