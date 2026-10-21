namespace Bot.Configuration
{
    public interface IConfigurationPubnub
    {
        string GetSubscriberKey();
        string GetPublisherKey();
        string GetSecretKey();
    }
}