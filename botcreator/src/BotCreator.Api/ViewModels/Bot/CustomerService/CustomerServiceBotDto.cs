namespace BotCreator.Api.ViewModels.Bot
{
    public class CustomerServiceBotDto : BotDto
    {
        public string ButtonText1 { get; set; }

        public string ResultText1 { get; set; }

        public string ButtonText2 { get; set; }

        public string ResultText2 { get; set; }

        public string ButtonText3 { get; set; }

        public string ResultText3 { get; set; }

        public bool WhatsappChecked { get; set; }

        public string WhatsappNumber { get; set; }

        public bool ChatOnWeb { get; set; }

        public bool FbChecked { get; set; }

        public string FbUserName { get; set; }

        public bool TalkToMeChecked { get; set; }

        public string TalkToAgent { get; set; }

        public bool CallMeChecked { get; set; }

        public string CallMeEmail { get; set; }

        public string Language { get; set; }
    }
}
