using System.Collections.Generic;
using Bot.telegram.funnel.Actions;
using Xunit;

namespace Bot.telegram.funnel.test
{
    public class StringExtensionsTest
    {
        private const string StartMessage = "\\start";
        private const string WelcomeMessage = "Welcome to CryptoTraders";
        private const string WriteEmailAddress = "Please write your email:";
        private const string MainMenu = "Welcome to CryptoTraders here is your options;";
        private const string ButtonBuyCourse = "Buy Course";
        private const string ButtonGoBack = "Go Back";
        private const string CourseSelectionMessage = "Which course would you like to buy:";
        private const string ButtonBegginer = "Begginer Trading";
        private const string ButtonIntermidiate = "Intermidiate Trading";
        private const string ButtonAdvance = "Advance Trading";
        private const string ButtonPro = "Proffesional Trading";

        [Fact]
        public void TheStrigGetReplaced()
        {
            Dictionary<string,object> parameters = new Dictionary<string, object>();
            parameters.AddBuyerAmount(1.2m);
            parameters.AddBuyerCurrency("LTC");
            parameters.AddWalletAddress("OJD990JERF09J9FJ09WJEFIJ09WJEF9J9SDJF09JSDF");

            string message = $"To complete your subscription you need to send a payment to the following address: \n" +
                      $"[code][WalletAddress][/code] \n\n" +
                      $"Send [BuyerAmount] [BuyerCurrency] \n" +
                      $"We will automatically process your payment";

            var result = message.ReplaceParameters(parameters);

            Assert.Contains("Send 1.2 LTC \n", result);
        }
    }
}
