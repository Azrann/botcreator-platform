using System;
using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class ClientServiceTest
    {
        [Fact]
        public void WhenIAddAClientICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            
            service.AddClient(client);

            var result = service.GetClientsByBotId(client.BotId);
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void WhenIAddAClientICanRetrieveItById()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);

            service.AddClient(client);

            var result = service.GetClient(client.BotId, client.User.Id);
            Assert.True(result.User.Id == client.User.Id);
        }

        [Fact]
        public void WhenIAddAUserFor2BotsICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client1 = GetClient(user);
            var client2 = GetClient(user);

            service.AddClient(client1);
            service.AddClient(client2);

            var result1 = service.GetClientsByBotId(client1.BotId);
            var result2 = service.GetClientsByBotId(client2.BotId);
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 1);
        }

        [Fact]
        public void WhenIAddSameClientTwiceItIsOnlyAddedOnce()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);

            service.AddClient(client);
            service.AddClient(client);
            var result = service.GetClientsByBotId(client.BotId);
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void WhenIUpdateEmailICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);

            service.AddClient(client);

            var result = service.GetClientsByBotId(client.BotId).First();
            Assert.True(string.IsNullOrEmpty(result.UserEmail));

            string email = "user@example.invalid";
            service.UpdateClientEmail(result.BotId, result.User.Id, email);
            var result2 = service.GetClientsByBotId(client.BotId).First();
            Assert.True(result2.UserEmail == email);
        }

        [Fact]
        public void WhenIAddANewSubscriptionICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            var subscription = GetSubscription();
            service.AddClient(client);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0,2, subscription.Currency, subscription.Fee);

            var result = service.GetActiveSubscriptions(client.BotId, client.User.Id).First();
            Assert.True(result.ExpiryDate < DateTime.Now.AddDays(2));
        }

        [Fact]
        public void ICanRetrieveExpirySubscriptions()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            var subscription = GetSubscription();
            service.AddClient(client);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0, -2, subscription.Currency,subscription.Fee);

            var result1 = service.GetActiveSubscriptions(client.BotId,client.User.Id);
            var result2 = service.GetSubscriptions(client.BotId, client.User.Id);
            Assert.True(!result1.Any());
            Assert.True(result2.Count() == 1);
        }

        [Fact]
        public void WhenIRenewAExpiredSubscriptionICreateAnotherOne()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            var subscription = GetSubscription();
            service.AddClient(client);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0, -2, subscription.Currency, subscription.Fee);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0, 1, subscription.Currency, subscription.Fee);
            var result1 = service.GetActiveSubscriptions(client.BotId, client.User.Id);
            var result2 = service.GetSubscriptions(client.BotId, client.User.Id);
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 2);
        }

        [Fact]
        public void WhenIRenewANonExpiredSubscriptionIUpdateIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            var subscription = GetSubscription();
            service.AddClient(client);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0, 2, subscription.Currency, subscription.Fee);
            service.RenewSubscription(client.BotId,client.User.Id, subscription.Name, 0, 2, subscription.Currency, subscription.Fee);
            var result1 = service.GetActiveSubscriptions(client.BotId, client.User.Id);
            var result2 = service.GetSubscriptions(client.BotId, client.User.Id);
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 1);
        }

        [Fact]
        public void ICanRetrieveExpiryActiveSubscriptions()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IClientService service = new ClientService(connection);
            var user = GetTelegramUser();
            var client = GetClient(user);
            var subscription = GetSubscription();
            service.AddClient(client);
            service.RenewSubscription(client.BotId, client.User.Id, subscription.Name, 0, -2, subscription.Currency, subscription.Fee);

            var result1 = service.GetExpiredActiveSubscriptions();
            var result2 = service.GetSubscriptions(client.BotId, client.User.Id);
            Assert.Contains(result1, x => x.BotId == client.BotId);
            Assert.True(result2.Count() == 1);
            service.DeactivateSubscription(result1.First(x => x.BotId == client.BotId).Id);
            result1 = service.GetExpiredActiveSubscriptions();
            Assert.True(result1.All(x => x.BotId != client.BotId));
        }

        private Subscription GetSubscription()
        {
            return new Subscription()
            {
                Currency = "BTC",
                Fee = 0.02m,
                Name = "Palmpasta Signals"
            };
        }

        private TelegramUser GetTelegramUser()
        {
            Random rnd = new Random();
            return new TelegramUser()
            {
                Id = NameHelper.GenerateEmployeeLastname() + NameHelper.GenerateTeamName(),
                AccessHash = rnd.Next(99999999),
                FirstName = NameHelper.GenerateEmployeeFirstName(),
                LastName = NameHelper.GenerateEmployeeLastname(),
                UserName = NameHelper.GenerateEmployeeFirstName(),
            };
        }

        public Client GetClient(TelegramUser user)
        {
            Random rnd = new Random();
            return new Client()
            {
                BotId = $"{Guid.NewGuid().ToString().Replace("-","")}{Guid.NewGuid().ToString().Replace("-", "")}",
                Id = Guid.NewGuid(),
                ChatId = rnd.Next(1000000),
                User = user
            };
        }
    }
}
