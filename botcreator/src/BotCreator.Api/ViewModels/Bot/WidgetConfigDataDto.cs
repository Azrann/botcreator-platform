namespace BotCreator.Api.ViewModels.Bot
{
    public class WidgetConfigDataDto
    {
        public int BotId { get; set; }

        public string Language { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string ChatNow { get; set; }

        public string BubbleMessage { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }
    }

    public class WidgetConfigActiveDto
    {
        public int BotId { get; set; }

        public bool Active { get; set; }
    }
}
