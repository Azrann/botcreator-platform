using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;

namespace Bot.telegram.funnel
{
    [JsonConverter(typeof(FunnelConverter))]
    public class Funnel : IFunnel
    {
        public Funnel()
        {
            CallbackActions = new Dictionary<ITrigger, List<FunnelAction>>();
            MessageActions = new Dictionary<ITrigger, List<FunnelAction>>();
            EventActions = new Dictionary<ITrigger, List<FunnelAction>>();
        }

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Funnel));

        public Dictionary<ITrigger, List<FunnelAction>> CallbackActions;
        public Dictionary<ITrigger, List<FunnelAction>> MessageActions;
        public Dictionary<ITrigger, List<FunnelAction>> EventActions;

        public async Task<bool> ProcessCallbacks(Data data, Dictionary<string, object> parameters,
            IServiceProvider provider, Guid correlationId)
        {
            foreach (var trigger in CallbackActions.Keys.OrderByDescending(x => x.Priority))
            {
                if (trigger.Check(data))
                {
                    foreach (var action in CallbackActions[trigger])
                    {
                        try
                        {
                            action.CorrelationId = correlationId;
                            await ExecuteAction(parameters, provider, action);
                        }
                        catch (Exception ex)
                        {
                            Log.LogErr($"ProcessCallbacks: Action execute error {ex.Message} {ex.StackTrace}",
                                correlationId);
                        }
                    }
                }
            }
            return true;
        }

        public async Task<bool> ProcessMessages(Data data, Dictionary<string, object> parameters,
            IServiceProvider provider, Guid correlationId)
        {
            foreach (var trigger in MessageActions.Keys.OrderByDescending(x => x.Priority))
            {
                if (trigger.Check(data))
                {
                    foreach (var action in MessageActions[trigger])
                    {
                        try
                        {
                            action.CorrelationId = correlationId;
                            await ExecuteAction(parameters, provider, action);
                        }
                        catch (Exception ex)
                        {
                            Log.LogErr($"ProcessMessages: Action execute error {JsonConvertExt.SerializeObject(ex)}",
                                correlationId);
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> ProcessEvents(Data data, Dictionary<string, object> parameters,
            IServiceProvider provider, Guid correlationId)
        {
            foreach (var trigger in EventActions.Keys.OrderByDescending(x => x.Priority))
            {
                if (trigger.Check(data))
                {
                    foreach (var action in EventActions[trigger])
                    {
                        try
                        {
                            action.CorrelationId = correlationId;
                            await ExecuteAction(parameters, provider, action);
                        }
                        catch (Exception ex)
                        {
                            Log.LogErr($"ProcessEvents: Action execute error {JsonConvertExt.SerializeObject(ex)}",
                                correlationId);
                        }
                    }
                }
            }

            return true;
        }

        private static async Task ExecuteAction(Dictionary<string, object> parameters, IServiceProvider provider,
            FunnelAction action)
        {
            var actionName = action.GetType().Name;
            if (actionName.Contains("Async"))
            {
                Type typeOfActionProcessor =
                    Type.GetType(
                        $"Bot.telegram.funnel.Actions.ActionProcessorAsync`1[Bot.telegram.funnel.Actions.{action.GetType().Name}],Bot.telegram.funnel");
                dynamic myAction = Convert.ChangeType(action, action.GetType());
                dynamic processor = provider.GetService(typeOfActionProcessor);
                await processor.ExecuteActionAsync(myAction, parameters);
            }
            else
            {
                Type typeOfActionProcessor =
                    Type.GetType(
                        $"Bot.telegram.funnel.Actions.ActionProcessor`1[Bot.telegram.funnel.Actions.{action.GetType().Name}],Bot.telegram.funnel");
                dynamic myAction = Convert.ChangeType(action, action.GetType());
                dynamic processor = provider.GetService(typeOfActionProcessor);
                processor.ExecuteAction(myAction, parameters);
            }
        }
    }

    public interface IFunnel
    {
        Task<bool> ProcessCallbacks(Data data, Dictionary<string, object> parameters, IServiceProvider provider,
            Guid correlationId);
        Task<bool> ProcessMessages(Data data, Dictionary<string, object> parameters, IServiceProvider provider,
            Guid correlationId);
        Task<bool> ProcessEvents(Data data, Dictionary<string, object> parameters, IServiceProvider provider,
            Guid correlationId);
    }
}