using Microsoft.Extensions.Options;

namespace Bot.Configuration
{
    public class ConfigurationPubnub : IConfigurationPubnub
    {
        private readonly IOptions<PubnubConfiguration> _config;


        public ConfigurationPubnub(IOptions<PubnubConfiguration> config)
        {
            _config = config;
        }

        public string GetSubscriberKey()
        {
            return _config.Value.SubscriberKey;
        }

        public string GetPublisherKey()
        {
            return _config.Value.PublisherKey;
        }

        public string GetSecretKey()
        {
            return _config.Value.SecretKey;
        }
    }
}