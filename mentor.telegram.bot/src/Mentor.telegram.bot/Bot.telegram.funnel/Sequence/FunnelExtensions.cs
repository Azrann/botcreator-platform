using System;
using System.Collections.Generic;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel
{
    public static class FunnelExtensions
    {
        public static Funnel AddSequenceState(this Funnel funnel, SequenceState sequence)
        {
            if (sequence.Steps.Count < 2)
            {
                throw new Exception("Sequence must have at least 2 elements");
            }

            for (int i = 0; i < sequence.Steps.Count; i++)
            {
                if (i == 0)
                {
                    var sequenceStep = sequence.Steps[i];
                    sequenceStep.AddToFunnelAsFirst(funnel, sequence.Id, sequence.FirstStepCallback);
                }
                else
                {
                    var sequenceStep = sequence.Steps[i];
                    sequenceStep.AddToFunnel(funnel, sequence.Id, i);
                }
            }

            sequence.MainMenuActions.Add(new StateUpdateAction(sequence.Id, ""));
            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SequenceState.MainMenu),
                    new StateIsNotValueTrigger(sequence.Id, "")
                }
            }, sequence.MainMenuActions);

            return funnel;
        }

        public static Funnel AddSequence(this Funnel funnel, Sequence sequence)
        {
            if (sequence.Steps.Count < 2)
            {
                throw new Exception("Sequence must have at least 2 elements");
            }

            for (int i = 0; i < sequence.Steps.Count; i++)
            {
                if (i == 0)
                {
                    AddStep(funnel, sequence.Steps[0].Message,
                        sequence.Steps[0].ButtonText, sequence.StartingButton, sequence.Id, i, sequence.FirstActions);
                }
                else if (i == sequence.Steps.Count-1)
                {
                    AddStep(funnel, sequence.Steps[i].Message,
                        sequence.Steps[i].ButtonText, sequence.Steps[i-1].ButtonText, sequence.Id, i, finalActions: sequence.FinalActions);
                }
                else
                {
                    AddStep(funnel, sequence.Steps[i].Message,
                        sequence.Steps[i].ButtonText, sequence.Steps[i - 1].ButtonText, sequence.Id, i);
                }
            }

            return funnel;
        }

        private static void AddStep(Funnel funnel, string message, string buttonText, string previousButtonText, string sequenceId, int i,
            List<FunnelAction> firstActions = null, List<FunnelAction> finalActions = null)
        {
            funnel.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger(sequenceId, i.ToString())
                }
            }, new List<FunnelAction>()
            {
                GetStepMessage(message,buttonText)
            });

            var funnelActions = new List<FunnelAction>()
            {
                new StateUpdateAction(sequenceId, (i+1).ToString()),
                GetStepMessage(message,buttonText)
            };

            if (finalActions != null)
            {
                funnelActions.AddRange(finalActions);
            }
            if (firstActions != null)
            {
                funnelActions.AddRange(firstActions);
            }

            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(previousButtonText),
                    new StateIsValueTrigger(sequenceId, i.ToString())
                }
            }, funnelActions);
        }

        private static FunnelAction GetStepMessage(string messageText, string buttonText)
        {
            return new MessageButtonAction(messageText, new List<List<string>>()
            {
                new List<string>()
                    {buttonText}
            }, true);
        }

        public static Funnel AddMenuText(this Funnel funnel, string menuButtonTriggerText, string menuText,
            string goBackText, ITrigger newTrigger = null)
        {
            var trigger = new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(menuButtonTriggerText),                   
                }
            };

            if (newTrigger != null)
            {
                trigger.Triggers.Add(newTrigger);
            }

            funnel.MessageActions.Add(trigger, new List<FunnelAction>()
            {
                new MessageMenuButtonAction(menuText, new List<List<string>>()
                {
                    new List<string>()
                        {goBackText}
                }, true)
            });
            return funnel;
        }
    }
}