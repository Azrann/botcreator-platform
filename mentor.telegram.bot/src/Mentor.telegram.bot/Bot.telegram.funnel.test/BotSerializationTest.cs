using System;
using System.Collections.Generic;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;
using Xunit;

namespace Bot.telegram.funnel.test
{
    public class BotSerializationTest
    {
        private const string StartMessage = "\\start";

        [Fact]
        public void ICanSerializeDeserializeAnEmptyBot()
        {
            funnel.Bot testBot = GetEmptyBot();
            var jsonBot = JsonConvert.SerializeObject(testBot);
            var Bot = JsonConvert.DeserializeObject<funnel.Bot>(jsonBot);
            Assert.True(Bot.TriggersToFunnel.Count == 0);
        }

        [Fact]
        public void ICanSerializeDeserializeWelcomeBot()
        {
            funnel.Bot testBot = GetWelcomeBot();
            var jsonBot = JsonConvert.SerializeObject(testBot);
            var Bot = JsonConvert.DeserializeObject<funnel.Bot>(jsonBot);
            Assert.True(Bot.Id == testBot.Id);
            Assert.True(Bot.AdminId == testBot.AdminId);
            Assert.True(Bot.TriggersToFunnel.Count == 1);
        }

       [Fact]
        public void ICanSerializeDeserializeComplexTriggerBot()
        {
            funnel.Bot testBot = GetComplexBotTrigger();
            var jsonBot = JsonConvert.SerializeObject(testBot);
            var Bot = JsonConvert.DeserializeObject<funnel.Bot>(jsonBot);
            Assert.True(Bot.TriggersToFunnel.Count == 2);
        }

        [Fact]
        public void ICanSerializeDeserializeReallyComplexTriggerBot()
        {
            funnel.Bot testBot = GetReallyComplexBotTrigger();
            var jsonBot = JsonConvert.SerializeObject(testBot);
            var Bot = JsonConvert.DeserializeObject<funnel.Bot>(jsonBot);
            Assert.True(Bot.TriggersToFunnel.Count == 1);
        }
        
        private funnel.Bot GetEmptyBot()
        {
            var result = new funnel.Bot();
            return result;
        }

        private funnel.Bot GetWelcomeBot()
        {
            var result = new funnel.Bot()
            {
                Id = "ABCDEEF",
                AdminId = Guid.NewGuid(),
            };
            result.TriggersToFunnel.Add(new MessageTrigger(StartMessage),Guid.NewGuid());
            return result;
        }
        
        private funnel.Bot GetComplexBotTrigger()
        {
            ITrigger comlex1 = new AndTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            ITrigger comlex2 = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            var result = new funnel.Bot();
            result.TriggersToFunnel.Add(comlex1, Guid.NewGuid());
            result.TriggersToFunnel.Add(comlex2, Guid.NewGuid());
            return result;
        }

        private funnel.Bot GetReallyComplexBotTrigger()
        {
            ITrigger complexChildTrigger = new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            ITrigger complexTrigger1 = new OrTrigger()
            {
                Priority = 80,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                    complexChildTrigger
                }
            };
            var result = new funnel.Bot();
            
            result.TriggersToFunnel.Add(complexTrigger1, Guid.NewGuid());
            return result;
        }
    }
}
