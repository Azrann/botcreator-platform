namespace Bot.Configuration
{
    public interface IConfigurationCommon
    {
        string Environment { get; }
        bool IsDevelopment();
        bool IsIvoProduction();
        bool IsAzProduction();
    }
}