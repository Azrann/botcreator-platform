using System;
using System.Collections.Generic;
using Core.Common.Extensions;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel
{
    public class StepConditionalWithState : IStepStates
    {
        public List<StepConditionalData> StateConditions { get; set; }
        public bool LastStep { get; set; }

        public StepConditionalWithState(bool lastStep = false)
        {
            LastStep = lastStep;
        }

        public void AddToFunnel(Funnel funnel, string sequenceId, int i)
        {
            foreach (var stepConditionalData in StateConditions)
            {
                AddToFunnelWithConditionalData(funnel, sequenceId, i, stepConditionalData);
            }
        }

        private void AddToFunnelWithConditionalData(Funnel funnel, string sequenceId, int currentStepNumber, StepConditionalData stepConditionalData)
        {
            var stepActions = stepConditionalData.Actions;
            var stepActionsJson = JsonConvertExt.SerializeObject(stepConditionalData.Actions);
            stepActions.Add(LastStep
                ? new StateUpdateAction(sequenceId, string.Empty)
                : new StateUpdateAction(sequenceId, stepConditionalData.NextStep.ToString()));


            //  stepActions.Add(new MessageAction($"Current state = {currentStepNumber} updating to { stepConditionalData.NextStep.ToString()} menu actions to show are {stepActionsJson} \n\n" +
            // $"StateName={stepConditionalData.StateName} .StateValue={stepConditionalData.StateValue}", false));


            var triggers = new List<ITrigger>()
            {
                new MessageIsNotTrigger(new List<string>()
                {
                    SequenceState.MainMenu,SequenceState.Back
                }),
                new StateIsValueTrigger(sequenceId, currentStepNumber.ToString()),
                new StateIsValueTrigger(stepConditionalData.StateName,stepConditionalData.StateValue)
            };

            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = triggers
            }, stepActions);

            funnel.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = triggers
            }, stepActions);

            var previousStepActions = stepConditionalData.PreviousStepActions;
            previousStepActions.Add(new StateUpdateAction(sequenceId, stepConditionalData.PreviousStep.ToString()));
            funnel.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SequenceState.Back),
                    new StateIsValueTrigger(sequenceId, currentStepNumber.ToString()),
                    new StateIsValueTrigger(stepConditionalData.StateName,stepConditionalData.StateValue)
                }
            }, previousStepActions);
        }

        public void AddToFunnelAsFirst(Funnel funnel, string sequenceId, string firstStepCallback)
        {
            throw new Exception("First Step should not be a conditional STEP");
        }
    }
}