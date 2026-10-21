using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mentor.telegram.bot.models;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;

namespace Mentor.bot.boundedContext.Services
{
    public class LiveChatQueue : ILiveChatQueue
    {
        private readonly string _connectionString;
        private readonly string _environment;

        public LiveChatQueue(string connectionString, string environment)
        {
            _connectionString = connectionString;
            _environment = environment;
        }

        private CloudQueueClient GetClient()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            return storageAccount.CreateCloudQueueClient();
        }

        public async Task<int> AddClientToQueue(QueueItem item)
        {
            var client = GetClient();
            CloudQueue queue = client.GetQueueReference(ParseQueueName($"{_environment}{item.BotId}"));

            queue.CreateIfNotExists();
            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(item)));

            return queue.ApproximateMessageCount ?? 0;
        }

        public async Task<QueueItem> GetClientFromQueue(string botId)
        {
            var client = GetClient();
            CloudQueue queue = client.GetQueueReference(ParseQueueName($"{_environment}{botId}"));

            queue.CreateIfNotExists();
            var message = await queue.GetMessageAsync();

            if (message != null)
            {
                await queue.DeleteMessageAsync(message);
                return JsonConvert.DeserializeObject<QueueItem>(message.AsString);
            }

            return null;
        }

        public string ParseQueueName(string name)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(name, "").ToLower();
        }
    }

    public interface ILiveChatQueue
    {
        Task<int> AddClientToQueue(QueueItem item);
        Task<QueueItem> GetClientFromQueue(string botId);
    }
}
