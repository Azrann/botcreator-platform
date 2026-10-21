using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class IvoSendAboutToExpireNotificationAsync : FunnelAction
    {
        public List<ExpirationData> ExpirationData { get;set;} 
    }
}
