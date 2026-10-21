using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public abstract class ActionProcessor<T> where T : FunnelAction
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Funnel));

        public abstract bool Execute(T action, Dictionary<string, object> parameters);

        public bool ExecuteAction(T action, Dictionary<string, object> parameters)
        {
            try
            {
                var actionName = action.GetType().Name;
                Log.LogInf($"{actionName} Executing.", action.CorrelationId);
                var result = Execute(action, parameters);
                Log.LogInf($"{actionName} Finished.", action.CorrelationId);
                return result;
            }
            catch (Exception e)
            {
                Log.LogErr($"{action.GetType().Name} with message {e.Message}.", action.CorrelationId);
                Log.LogErr($"{action.GetType().Name} exception {JsonConvertExt.SerializeObject(e)}.", action.CorrelationId);
                File.WriteAllText(@"App_Data/logs/errors.txt", $"{DateTime.Now} *************\n\n{action.GetType().Name} exception {JsonConvertExt.SerializeObject(e)}");
                return false;
            }
        }
    }

    public abstract class ActionProcessorAsync<T> where T : FunnelAction
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Funnel));

        public abstract Task<bool> ExecuteAsync(T action, Dictionary<string, object> parameters);

        public async Task<bool> ExecuteActionAsync(T action, Dictionary<string, object> parameters)
        {
            try
            {
                var actionName = action.GetType().Name;
                Log.LogInf($"{actionName} Async Executing.", action.CorrelationId);
                var result = await ExecuteAsync(action, parameters);
                Log.LogInf($"{actionName} Async Finished.", action.CorrelationId);
                return result;
            }
            catch (Exception e)
            {
                Log.LogErr($"{action.GetType().Name} with message {e.Message}.", action.CorrelationId);
                Log.LogErr($"{action.GetType().Name} exception {JsonConvertExt.SerializeObject(e)}.", action.CorrelationId);
                return false;
            }
        }
    }
}