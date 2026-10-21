using System;
using System.Threading;
using Bot.Configuration;
using Core.Common.Extensions;
using log4net;
using PubNubMessaging.Core;
using Mentor.telegram.bot.boundedContext.Services;

namespace Mentor.telegram.bot.api
{
    public class PubnubMessageBus : IPubnubMessageBus
    {
        Pubnub _pubnub;
        private readonly IConfigurationPubnub _pubnubConfig;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(PubnubMessageBus));
        private Guid CorrelationId;

        public PubnubMessageBus(IConfigurationPubnub pubnubConfig, bool debugAll = false)
        {
            _pubnubConfig = pubnubConfig;
            var subscriberKey = _pubnubConfig.GetSubscriberKey();
            var publisherKey = _pubnubConfig.GetPublisherKey();
            
            if (string.IsNullOrEmpty(subscriberKey))
            {
                throw new Exception("PubNub REPLACE_WITH_YOUR_VALUE is missing from the _config file.");
            }
            if (string.IsNullOrEmpty(publisherKey))
            {
                throw new Exception("PubNub REPLACE_WITH_YOUR_VALUE is missing from the _config file.");
            }

            _logger.Info("PubnubMessageBus configured");
        }

        public IPubnubMessageBus Start(Guid correlationId)
        {
            CorrelationId = correlationId;
            _logger.LogInf("PubnubMessageBus Start called", CorrelationId);
            _pubnub = new Pubnub(_pubnubConfig.GetPublisherKey(),_pubnubConfig.GetSubscriberKey(),_pubnubConfig.GetSecretKey()) { };
            _pubnub.ChangeUUID(Guid.NewGuid().ToString());
            _logger.LogInf("PubnubMessageBus started", CorrelationId);
            return this;
        }

        public void Publish(object message, string channelId)
        {
            _logger.LogInf($"PubnubMessageBus publish {JsonConvertExt.SerializeObject(message)} to channelId:{channelId}", CorrelationId);
            _pubnub.Publish(channelId, message,
                DisplayReturnMessage,
                DisplayErrorMessage
            );
        }

        private void DisplayReturnMessage(object obj)
        {
            _logger.LogInf(JsonConvertExt.SerializeObject(obj), CorrelationId);
        }

        void DisplayErrorMessage(PubnubClientError pubnubError)
        {
            _logger.LogErr(JsonConvertExt.SerializeObject(pubnubError), CorrelationId);
        }

        public void Grantd(string authKey, string channel, bool read, bool write, int ttl = 240)
        {
            ManualResetEvent grantManualEvent = new ManualResetEvent(false);
            _logger.LogInf("Called Grant access.", CorrelationId);
            _pubnub.GrantAccess<string>(channel, authKey, read, write, ttl, DisplayReturnMessage, DisplayErrorMessage);
        }
    }

    public class PlatformPubnubLog : IPubnubLog
    {
        private readonly ILog _logger;

        public PlatformPubnubLog(ILog logger)
        {
            _logger = logger;
        }

        public void WriteToLog(string log)
        {
            _logger.Debug(log);
        }

        public LoggingMethod.Level LogLevel { get; set; }
    }

    /*public class PlatformPubnubLog2 : IPubnubLog
    {
        private readonly string _logFilePath = "";
        public PlatformPubnubLog2()
        {
            _logFilePath = $@"C:\Test\configFiles\Pubnubmessaging{Guid.NewGuid()}.log";
        }

        public void WriteToLog(string log)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(_logFilePath, true))
            {
                file.WriteLine(log);
            }
        }
    }*/
}
