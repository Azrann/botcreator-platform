using System.Collections.Generic;
using Core.Common.Extensions;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel
{
    public class StepWithState : IStepStates
    {
        public List<FunnelAction> Actions { get; set; }
        public List<FunnelAction> PreviousStepActions { get; set; }
        public bool LastStep { get; set; }

        public StepWithState(bool lastStep = false)
        {
            LastStep = lastStep;
        }

        public void AddToFunnel(Funnel funnel, string sequenceId, int i)
        {
            var stepActionsJson = JsonConvertExt.SerializeObject(Actions);
            var stepActions = Actions;
            stepActions.Add(LastStep
                ? new StateUpdateAction(sequenceId, string.Empty)
                : new StateUpdateAction(sequenceId, (i + 1).ToString()));

            // stepActions.Add(new MessageAction($"Current state = {i} updating to {i +1} menu actions to show are {stepActionsJson}", false));

            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100-i,
                Triggers = new List<ITrigger>()
                {
                    new MessageIsNotTrigger(new List<string>()
                    {
                        SequenceState.MainMenu,SequenceState.Back
                    }),
                    new StateIsValueTrigger(sequenceId, i.ToString())
                }
            }, Actions);

            funnel.MessageActions.Add(new AndTrigger()
            {
                Priority = 100 - i,
                Triggers = new List<ITrigger>()
                {
                    new MessageIsNotTrigger(new List<string>()
                    {
                        SequenceState.MainMenu,SequenceState.Back
                    }),
                    new StateIsValueTrigger(sequenceId, i.ToString())
                }
            }, Actions);

            // var previoustepactionsJson = JsonConvertExt.SerializeObject(PreviousStepActions);
            var previousStepActions = PreviousStepActions;
            previousStepActions.Add(new StateUpdateAction(sequenceId, (i - 1).ToString()));
            //  previousStepActions.Add(new MessageAction($"Current state = {i} updating to {i-1} menu actions to show are {previoustepactionsJson}",false));

            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100 - i,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SequenceState.Back),
                    new StateIsValueTrigger(sequenceId, i.ToString()) //Notice i+1 because when 
                }
            }, previousStepActions);
        }

        public void AddToFunnelAsFirst(Funnel funnel, string sequenceId, string firstStepCallback)
        {
            var stepActions = Actions;
            stepActions.Add(new StateUpdateAction(sequenceId, "1"));

            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageIsNotTrigger(new List<string>()
                    {
                        SequenceState.MainMenu, SequenceState.Back
                    }),
                    new StateIsValueTrigger(sequenceId, ""),
                    new MessageTrigger(firstStepCallback)
                }
            }, Actions);
        }
    }
}