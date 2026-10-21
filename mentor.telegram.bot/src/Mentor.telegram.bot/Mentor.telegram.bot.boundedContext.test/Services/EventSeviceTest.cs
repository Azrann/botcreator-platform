using System;
using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class EventSeviceTest
    {
        string connection = @"REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void WhenIAddAEventItemICanRetrieveItByBotId()
        {
            IEventService service = new EventService(connection);

            var eventItem = GetEventItem();
            
            service.AddEvent(eventItem);

            var result = service.GetEventsByBotId(eventItem.BotId);
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void WhenIAddAEventItemICanRetrieveIt()
        {
            IEventService service = new EventService(connection);

            var eventItem = GetEventItem();

            service.AddEvent(eventItem);

            var result = service.GetEventsByUsername(eventItem.BotId, eventItem.Username);
            Assert.NotNull(result);
        }

        private EventItem GetEventItem()
        {
            var randomObject = new
            {
                Property1 = "test1",
                Property2 = "test2",
            };
            Random rnd = new Random();
            return new EventItem()
            {
                BotId = $"{Guid.NewGuid().ToString().Replace("-","")}{Guid.NewGuid().ToString().Replace("-", "")}",
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                ChannelId = rnd.Next(1000000).ToString(),
                DataJson = JsonConvert.SerializeObject(randomObject),
                Name ="Alberto_M_test",
                Username = Guid.NewGuid().ToString().Replace("-",""),
            };
        }
    }
}
