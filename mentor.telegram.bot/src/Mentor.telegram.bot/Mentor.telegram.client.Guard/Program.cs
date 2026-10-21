using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenTl.Schema;
using OpenTl.Schema.Channels;
using TChannelParticipant = OpenTl.Schema.TChannelParticipant;

namespace Mentor.telegram.client.Guard
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var result = bot.FindChatData("Verified Crypto Chat 🤳").Result;
            //client.UpdatesService.RecieveUpdates += Updates;
            var request = new RequestGetParticipants()
            {
                Channel = new TInputChannel()
                {
                    AccessHash = result.AccessHash,
                    ChannelId = result.ChatId
                },
                Filter = new TChannelParticipantsRecent(),
                Limit = 10,
                Offset = 0
            };
           
            var result2 = (TChannelParticipants) client.CustomRequestsService.SendRequestAsync(request).Result;
            List<Tuple<int, DateTime>> listUsers = new List<Tuple<int, DateTime>>();

            foreach (var channelParticipant in result2.Participants.OfType<TChannelParticipant>())
            {
                var tup = new Tuple<int, DateTime>(channelParticipant.UserId, UnixTimeStampToDateTime(channelParticipant.Date));
                listUsers.Add(tup);
            }

            Console.WriteLine("Press escape to exit");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Press escape to exit");
            }
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private static Task Updates(IUpdates update)
        {
            // handle updates
            switch (update)
            {
                case TUpdates updates:
                    Console.WriteLine(JsonConvert.SerializeObject(updates));
                    break;
                case TUpdatesCombined updatesCombined:
                    Console.WriteLine(JsonConvert.SerializeObject(updatesCombined));
                    break;
                case TUpdateShort updateShort:
                    Console.WriteLine(JsonConvert.SerializeObject(updateShort));
                    break;
                case TUpdateShortChatMessage updateShortChatMessage:
                    Console.WriteLine(JsonConvert.SerializeObject(updateShortChatMessage));
                    break;
                case TUpdateShortMessage updateShortMessage:
                    Console.WriteLine(JsonConvert.SerializeObject(updateShortMessage));
                    break;
                case TUpdateShortSentMessage updateShortSentMessage:
                    Console.WriteLine(JsonConvert.SerializeObject(updateShortSentMessage));
                    break;
                case TUpdatesTooLong updatesTooLong:
                    Console.WriteLine(JsonConvert.SerializeObject(updatesTooLong));
                    break;
                default:
                    Console.WriteLine(JsonConvert.SerializeObject(update));
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
