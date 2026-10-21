namespace BotCreator.Api.ViewModels.Bot
{
    public class UserBotDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TemplateName { get; set; }

        public bool IsDeployed { get; set; }

        public bool IsActive { get; set; }

        public string ExternalId { get; set; }
    }
}
