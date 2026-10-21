using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using System;
using System.Collections.Generic;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public static class DeepakTestBot
    {
        public static Funnel GetFunnel()
        {
            var testBotfunnel = new Funnel();
            testBotfunnel.MessageActions.Add(new MessageIsNotTrigger(new List<string> { "OverAndOut", "bla bla bla" }),
            new List<FunnelAction>() { new MessageAction("Ok",true , new Random().Next(0, 10)) });
            return testBotfunnel;
        }

    }
}
