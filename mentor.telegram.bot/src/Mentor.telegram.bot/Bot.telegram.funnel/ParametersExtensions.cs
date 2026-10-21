using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Binance.Net.Objects;
using CryptoCore.Model;

namespace Bot.telegram.funnel
{
    public static class ParametersExtensions
    {
        private const string BotId = "BotId";
        private const string MessageText = "MessageText";
        private const string UserId = "UserId";
        private const string UserIdCommand = "UserIdCommand";
        private const string AdminId = "adminId";
        public const string Username = "REPLACE_WITH_YOUR_VALUE";
        public const string UsernameCommand = "REPLACE_WITH_YOUR_VALUE";
        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        public const string ChatId = "ChatId";
        public const string ChatIdCommand = "ChatIdCommand";
        private const string PaymentStatus = "PaymentStatus";
        private const string BuyerAmount = "BuyerAmount";
        private const string OriginalAmount = "OriginalAmount";
        private const string ReceivedAmount = "ReceivedAmount";
        private const string ReceivedConfirms = "ReceivedConfirms";
        private const string BuyerCurrency = "BuyerCurrency";
        private const string OriginalCurrency = "OriginalCurrency";
        private const string WalletAddress = "WalletAddress";
        private const string TransactionId = "TransactionId";
        private const string ItemName = "ItemName";
        private const string SubscriptionId = "SubscriptionId";
        private const string Channels = "Channels";
        private const string PaymentUrl = "PaymentUrl";
        private const string CorrelationId = "CorrelationId";
        private const string BiOpenTrades = "BiOpenTrades";
        private const string BiMarketData = "BiMarketData";
        private const string BiMessageResult = "BiMessageResult";
        private const string CallbackData = "CallbackData";
        private const string UserHash = "UserHash";
        private const string UserHashCommand = "UserHashCommand";
        private const string PageToken = "PageToken";
        private const string AppSecret = "REPLACE_WITH_YOUR_VALUE";
        private const string ClientId = "REPLACE_WITH_YOUR_VALUE";
        private const string Language = "Language";

        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetBotId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BotId))
            {
                return (string)parameters[BotId];
            }

            throw new ArgumentException($"BotId not found in parameters");
        }

        public static void AddBotId(this Dictionary<string, object> parameters, string botId)
        {
            if (parameters.ContainsKey(BotId))
            {
                parameters[BotId] = botId;
                return;
            }
            parameters.Add(BotId, botId);
        }
       
        public static string GetMessageText(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(MessageText))
            {
                return (string)parameters[MessageText];
            }

            return null;
        }

        public static void AddMessageText(this Dictionary<string, object> parameters, string messageText)
        {
            if (parameters.ContainsKey(MessageText))
            {
                parameters[MessageText] = messageText;
                return;
            }
            parameters.Add(MessageText, messageText);
        }

        public static string GetCallbackData(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(CallbackData))
            {
                return (string)parameters[CallbackData];
            }

            return null;
        }

        public static void AddCallbackData(this Dictionary<string, object> parameters, string callbackData)
        {
            if (parameters.ContainsKey(CallbackData))
            {
                parameters[CallbackData] = callbackData;
                return;
            }
            parameters.Add(CallbackData, callbackData);
        }

        public static string GetUserId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(UserId))
            {
                return (string)parameters[UserId];
            }

            throw new ArgumentException($"UserId not found in parameters");
        }

        public static void AddUserId(this Dictionary<string, object> parameters, string userId)
        {
            if (parameters.ContainsKey(UserId))
            {
                parameters[UserId] = userId;
                return;
            }
            parameters.Add(UserId, userId);
        }

        public static long GetUserHash(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(UserHash))
            {
                return (int)parameters[UserHash];
            }

            throw new ArgumentException($"UserHash not found in parameters");
        }

        public static void AddUserHash(this Dictionary<string, object> parameters, long userHash)
        {
            if (parameters.ContainsKey(UserHash))
            {
                parameters[UserHash] = userHash;
                return;
            }
            parameters.Add(UserHash, userHash);
        }

        public static long GetUserHashCommand(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(UserHashCommand))
            {
                return (int)parameters[UserHashCommand];
            }

            throw new ArgumentException($"UserHashCommand not found in parameters");
        }

        public static void AddUserHashCommand(this Dictionary<string, object> parameters, long userHashCommand)
        {
            if (parameters.ContainsKey(UserHashCommand))
            {
                parameters[UserHashCommand] = userHashCommand;
                return;
            }
            parameters.Add(UserHashCommand, userHashCommand);
        }

        public static string GetadminId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(AdminId))
            {
                return (string)parameters[AdminId];
            }

            throw new ArgumentException($"AdminId not found in parameters");
        }

        public static void AddadminId(this Dictionary<string, object> parameters, string adminId)
        {
            if (parameters.ContainsKey(adminId))
            {
                parameters[AdminId] = adminId;
                return;
            }
            parameters.Add(AdminId, adminId);
        }
        public static void AddLanguage(this Dictionary<string, object> parameters, string language)
        {
            if (parameters.ContainsKey(language))
            {
                parameters[Language] = language;
                return;
            }
            parameters.Add(Language, language);
        }

        public static string GetLanguage(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Language))
            {
                return (string)parameters[Language];
            }

            throw new ArgumentException($"Language not found in parameters");
        }

        public static Guid GetClientId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ClientId))
            {
                return (Guid)parameters[ClientId];
            }

            throw new ArgumentException($"ClientId not found in parameters");
        }

        public static void AddClientId(this Dictionary<string, object> parameters, Guid clientId)
        {
            if (parameters.ContainsKey(ClientId))
            {
                parameters[ClientId] = clientId;
                return;
            }
            parameters.Add(ClientId, clientId);
        }

        public static string GetUsername(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Username))
            {
                return (string)parameters[Username];
            }

            throw new ArgumentException($"Username not found in parameters");
        }

        public static void AddUsername(this Dictionary<string, object> parameters, string username)
        {
            if (parameters.ContainsKey(Username))
            {
                parameters[Username] = username;
                return;
            }
            parameters.Add(Username, username);
        }

        public static string GetUsernameCommand(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(UsernameCommand))
            {
                return (string)parameters[UsernameCommand];
            }

            throw new ArgumentException($"UsernameCommand not found in parameters");
        }

        public static void AddUsernameCommand(this Dictionary<string, object> parameters, string username)
        {
            if (parameters.ContainsKey(UsernameCommand))
            {
                parameters[UsernameCommand] = username;
                return;
            }
            parameters.Add(UsernameCommand, username);
        }

        public static string GetFirstname(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Firstname))
            {
                return (string)parameters[Firstname];
            }

            throw new ArgumentException($"Firstname not found in parameters");
        }

        public static void AddFirstname(this Dictionary<string, object> parameters, string firstname)
        {
            if (parameters.ContainsKey(Firstname))
            {
                parameters[Firstname] = firstname;
                return;
            }
            parameters.Add(Firstname, firstname);
        }

        public static string GetLastname(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Lastname))
            {
                return (string)parameters[Lastname];
            }

            throw new ArgumentException($"Lastname not found in parameters");
        }

        public static void AddLastname(this Dictionary<string, object> parameters, string lastname)
        {
            if (parameters.ContainsKey(Lastname))
            {
                parameters[Lastname] = lastname;
                return;
            }
            parameters.Add(Lastname, lastname);
        }

        public static long GetChatId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ChatId))
            {
                return (long)parameters[ChatId];
            }

            if (parameters.ContainsKey(UserId))
            {
                return (long)parameters[UserId];
            }

            throw new ArgumentException($"ChatId not found in parameters");
        }

        public static void AddChatId(this Dictionary<string, object> parameters, long chatId)
        {
            if (parameters.ContainsKey(ChatId))
            {
                parameters[ChatId] = chatId;
                return;
            }
            parameters.Add(ChatId, chatId);
        }

        public static long GetChatIdCommand(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ChatIdCommand))
            {
                return (long)parameters[ChatIdCommand];
            }

            if (parameters.ContainsKey(UserIdCommand))
            {
                return (long)parameters[UserIdCommand];
            }

            throw new ArgumentException($"ChatIdCommand not found in parameters");
        }

        public static void AddChatIdCommand(this Dictionary<string, object> parameters, long chatId)
        {
            if (parameters.ContainsKey(ChatIdCommand))
            {
                parameters[ChatIdCommand] = chatId;
                return;
            }
            parameters.Add(ChatIdCommand, chatId);
        }

        public static int GetPaymentStatus(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(PaymentStatus))
            {
                return (int)parameters[PaymentStatus];
            }

            throw new ArgumentException($"PaymentStatus not found in parameters");
        }

        public static void AddPaymentStatus(this Dictionary<string, object> parameters, int paymentStatus)
        {
            if (parameters.ContainsKey(PaymentStatus))
            {
                parameters[PaymentStatus] = paymentStatus;
                return;
            }
            parameters.Add(PaymentStatus, paymentStatus);
        }

        public static decimal GetBuyerAmount(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BuyerAmount))
            {
                return (decimal)parameters[BuyerAmount];
            }

            throw new ArgumentException($"BuyerAmount not found in parameters");
        }

        public static void AddBuyerAmount(this Dictionary<string, object> parameters, decimal buyerAmount)
        {
            if (parameters.ContainsKey(BuyerAmount))
            {
                parameters[BuyerAmount] = buyerAmount;
                return;
            }
            parameters.Add(BuyerAmount, buyerAmount);
        }

        public static decimal GetOriginalAmount(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(OriginalAmount))
            {
                return (decimal)parameters[OriginalAmount];
            }

            throw new ArgumentException($"OriginalAmount not found in parameters");
        }

        public static void AddOriginalAmount(this Dictionary<string, object> parameters, decimal originalAmount)
        {
            if (parameters.ContainsKey(OriginalAmount))
            {
                parameters[OriginalAmount] = originalAmount;
                return;
            }
            parameters.Add(OriginalAmount, originalAmount);
        }
        public static decimal GetReceivedAmount(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ReceivedAmount))
            {
                return (decimal)parameters[ReceivedAmount];
            }

            throw new ArgumentException($"ReceivedAmount not found in parameters");
        }

        public static void AddReceivedAmount(this Dictionary<string, object> parameters, decimal receivedAmount)
        {
            if (parameters.ContainsKey(ReceivedAmount))
            {
                parameters[ReceivedAmount] = receivedAmount;
                return;
            }
            parameters.Add(ReceivedAmount, receivedAmount);
        }

        public static int GetReceivedConfirms(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ReceivedConfirms))
            {
                return (int)parameters[ReceivedConfirms];
            }

            throw new ArgumentException($"ReceivedConfirms not found in parameters");
        }

        public static void AddReceivedConfirms(this Dictionary<string, object> parameters, int receivedConfirms)
        {
            if (parameters.ContainsKey(ReceivedConfirms))
            {
                parameters[ReceivedConfirms] = receivedConfirms;
                return;
            }
            parameters.Add(ReceivedConfirms, receivedConfirms);
        }

        public static string GetBuyerCurrency(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BuyerCurrency))
            {
                return parameters[BuyerCurrency].ToString();
            }

            throw new ArgumentException($"BuyerCurrency not found in parameters");
        }

        public static void AddBuyerCurrency(this Dictionary<string, object> parameters, string buyerCurrency)
        {
            if (parameters.ContainsKey(BuyerCurrency))
            {
                parameters[BuyerCurrency] = buyerCurrency;
                return;
            }
            parameters.Add(BuyerCurrency, buyerCurrency);
        }

        public static string GetOriginalCurrency(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(OriginalCurrency))
            {
                return parameters[OriginalCurrency].ToString();
            }

            throw new ArgumentException($"OriginalCurrency not found in parameters");
        }

        public static void AddOriginalCurrency(this Dictionary<string, object> parameters, string originalCurrency)
        {
            if (parameters.ContainsKey(OriginalCurrency))
            {
                parameters[OriginalCurrency] = originalCurrency;
                return;
            }
            parameters.Add(OriginalCurrency, originalCurrency);
        }
        public static string GetWalletAddress(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(WalletAddress))
            {
                return parameters[WalletAddress].ToString();
            }

            throw new ArgumentException($"WalletAddress not found in parameters");
        }

        public static void AddWalletAddress(this Dictionary<string, object> parameters, string walletAddress)
        {
            if (parameters.ContainsKey(WalletAddress))
            {
                parameters[WalletAddress] = walletAddress;
                return;
            }
            parameters.Add(WalletAddress, walletAddress);
        }

        public static string GetPaymentUrl(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(PaymentUrl))
            {
                return parameters[PaymentUrl].ToString();
            }

            throw new ArgumentException($"PaymentUrl not found in parameters");
        }

        public static void AddPaymentUrl(this Dictionary<string, object> parameters, string paymentUrl)
        {
            if (parameters.ContainsKey(PaymentUrl))
            {
                parameters[PaymentUrl] = paymentUrl;
                return;
            }
            parameters.Add(PaymentUrl, paymentUrl);
        }

        public static string GetTransactionId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(TransactionId))
            {
                return parameters[TransactionId].ToString();
            }

            throw new ArgumentException($"TransactionId not found in parameters");
        }

        public static void AddTransactionId(this Dictionary<string, object> parameters, string transactionId)
        {
            if (parameters.ContainsKey(TransactionId))
            {
                parameters[TransactionId] = transactionId;
                return;
            }
            parameters.Add(TransactionId, transactionId);
        }

        public static string GetItemName(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(ItemName))
            {
                return parameters[ItemName].ToString();
            }

            throw new ArgumentException($"ItemName not found in parameters");
        }

        public static void AddItemName(this Dictionary<string, object> parameters, string itemName)
        {
            if (parameters.ContainsKey(ItemName))
            {
                parameters[ItemName] = itemName;
                return;
            }
            parameters.Add(ItemName, itemName);
        }


        public static List<long> GetChannels(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Channels))
            {
                return (List<long>)parameters[Channels];
            }

            throw new ArgumentException($"Channels not found in parameters");
        }

        public static void AddChannels(this Dictionary<string, object> parameters, List<long> channels)
        {
            if (parameters.ContainsKey(Channels))
            {
                parameters[Channels] = channels;
                return;
            }
            parameters.Add(Channels, channels);
        }

        public static Guid GetSubscriptionId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(SubscriptionId))
            {
                return new Guid(parameters[SubscriptionId].ToString());
            }

            throw new ArgumentException($"SubscriptionId not found in parameters");
        }

        public static void AddSubscriptionId(this Dictionary<string, object> parameters, Guid subscriptionId)
        {
            if (parameters.ContainsKey(SubscriptionId))
            {
                parameters[SubscriptionId] = subscriptionId;
                return;
            }
            parameters.Add(SubscriptionId, subscriptionId);
        }

        public static Guid GetCorrelationId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(CorrelationId))
            {
                return (Guid)parameters[CorrelationId];
            }

            throw new ArgumentException($"CorrelationId not found in parameters");
        }

        public static void AddCorrelationId(this Dictionary<string, object> parameters, Guid correlationId)
        {
            if (parameters.ContainsKey(CorrelationId))
            {
                parameters[CorrelationId] = correlationId;
                return;
            }
            parameters.Add(CorrelationId, correlationId);
        }
        public static PriceData GetBiMarketData(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BiMarketData))
            {
                return (PriceData)parameters[BiMarketData];
            }

            throw new ArgumentException($"BiMarketData not found in parameters");
        }

        public static void AddBiMarketData(this Dictionary<string, object> parameters, PriceData priceData)
        {
            if (parameters.ContainsKey(BiMarketData))
            {
                parameters[BiMarketData] = priceData;
                return;
            }
            parameters.Add(BiMarketData, priceData);
        }

        public static List<BinanceOrder> GetBiOpenTrades(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BiOpenTrades))
            {
                return (List<BinanceOrder>)parameters[BiOpenTrades];
            }

            throw new ArgumentException($"BiOpenTrades not found in parameters");
        }

        public static void AddBiOpenTrades(this Dictionary<string, object> parameters, List<BinanceOrder> openTrades)
        {
            if (parameters.ContainsKey(BiOpenTrades))
            {
                parameters[BiOpenTrades] = openTrades;
                return;
            }
            parameters.Add(BiOpenTrades, openTrades);
        }

        public static string GetBiMessageResult(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BiMessageResult))
            {
                return parameters[BiMessageResult].ToString();
            }

            throw new ArgumentException($"BiMessageResult not found in parameters");
        }

        public static void AddBiMessageResult(this Dictionary<string, object> parameters, string biMessageResult)
        {
            if (parameters.ContainsKey(BiMessageResult))
            {
                parameters[BiMessageResult] = biMessageResult;
                return;
            }
            parameters.Add(BiMessageResult, biMessageResult);
        }

        public static void AddAdditionalParameters(this Dictionary<string, object> parameters, Dictionary<string, object> additional)
        {
            if (parameters == null || additional == null || additional.Count == 0)
                return;

            foreach (var param in additional)
            {
                if (!parameters.ContainsKey(param.Key))
                {
                    parameters.Add(param.Key, param.Value);
                }
            }
        }
        public static string GetPageToken(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(PageToken))
            {
                return (string)parameters[PageToken];
            }

            throw new ArgumentException($"PageToken not found in parameters");
        }

        public static void AddPageToken(this Dictionary<string, object> parameters, string pageToken)
        {
            if (parameters.ContainsKey(PageToken))
            {
                parameters[PageToken] = pageToken;
                return;
            }
            parameters.Add(PageToken, pageToken);
        }
        public static string GetAppSecret(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(AppSecret))
            {
                return (string)parameters[AppSecret];
            }

            throw new ArgumentException($"AppSecret not found in parameters");
        }

        public static void AddAppSecret(this Dictionary<string, object> parameters, string appSecret)
        {
            if (parameters.ContainsKey(AppSecret))
            {
                parameters[AppSecret] = appSecret;
                return;
            }
            parameters.Add(AppSecret, appSecret);
        }
    }

    public static class EmojiExtensions
    {
        public static string EncodeNonAsciiCharacters(this string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        static string DecodeEncodedNonAsciiCharacters2(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
    }
}