using Microsoft.Extensions.Options;

namespace Bot.Configuration
{
    public class ConfigurationCommon : IConfigurationCommon
    {
        private readonly IOptions<CommonConfiguration> _config;

        public ConfigurationCommon(IOptions<CommonConfiguration> config)
        {
            _config = config;
        }

        public string Environment => _config.Value.Environment;

        public bool IsDevelopment()
        {
            if (Environment != null && Environment.ToLower() == "qa")
                return true;

            if (Environment != null && Environment.ToLower() == "dev")
                return true;
            return false;
        }

        public bool IsIvoProduction()
        {
            if (Environment != null && Environment.ToLower() == "ivo")
                return true;

            return false;
        }

        public bool IsAzProduction()
        {
            if (Environment != null && Environment.ToLower() == "az")
                return true;

            return false;
        }
    }
}
