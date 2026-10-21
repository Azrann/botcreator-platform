using Microsoft.Extensions.Options;

namespace Bot.Configuration
{
    public interface IConfigurationFunnel
    {
        string GetServerUrl { get; }
    }

    public class ConfigurationFunnel : IConfigurationFunnel
    {
        private readonly IOptions<BotConfiguration> _config;

        public string GetServerUrl => _config.Value.ServerUrl;

        public ConfigurationFunnel(IOptions<BotConfiguration> config)
        {
            _config = config;
        }
    }
}