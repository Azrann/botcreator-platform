using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.shared.BotTemplates
{
    public class WidgetSupportBot : IBotDefinition
    {
        public string ConnectingToAgentMessage { get; set; }
        public string PreMenuText { get; set; }

        public bool WhatsappChecked { get; set; }
        public string WhatsappNumber { get; set; }

        public bool ChatOnWeb { get; set; }

        public bool FbChecked { get; set; }
        public string FbUserName { get; set; }

        public bool CallMeChecked { get; set; }
        public string CallMeEmail { get; set; }
        public string CallMeButtonText { get; set; }
        public string TalkToAgent { get; set; }
        public Dictionary<string, string> MenuBot { get; set; }

        public Funnel GetFunnel()
        {
            var result = new Funnel();
            
            result.MessageActions.Add(new MessageTrigger("menu"),
                new List<FunnelAction>()
                {
                    new WidgetMessageButtonAction(PreMenuText, GetMenuList())
                });

            AddMenuCallback(result);
            return result;
        }

        private void AddMenuCallback(Funnel result)
        {
            foreach (var menu in MenuBot)
            {
                result.CallbackActions.Add(new MessageTrigger(menu.Key),
                    new List<FunnelAction>()
                    {
                        new WidgetMessageAction(menu.Value),
                        new WidgetMessageButtonAction(PreMenuText, GetMenuList())
                    });
            }

            if (ChatOnWeb)
            {
                result.CallbackActions.Add(new MessageTrigger(TalkToAgent),
                    new List<FunnelAction>()
                    {
                        new WidgetMessageAction(TalkToAgent),
                        new ClientRequesTalkActionAsync()
                    });
            }
            
            if (WhatsappChecked)
            {
                result.CallbackActions.Add(new MessageTrigger("WhatsApp"),
                    new List<FunnelAction>()
                    {
                        new WidgetMessageAction($"<a href='https://api.whatsapp.com/send?phone={WhatsappNumber}&text=Hi'>Talk on WhatsApp</a>"),
                    });
            }

            if (CallMeChecked)
            {
                result.CallbackActions.Add(new MessageTrigger(CallMeButtonText),
                    new List<FunnelAction>()
                    {
                        new StateUpdateAction("AddingPhone","true"),
                        new WidgetMessageForKeyAction("PleaseTypePhoneNumberMessage")
                    });
                result.MessageActions.Add(new StateIsValueTrigger("AddingPhone","true"), 
                    new List<FunnelAction>()
                    {
                        new StateUpdateAction("AddingPhone","false"),
                        new CallMeEmailAsync()
                        {
                            Email = CallMeEmail,
                        },
                        new WidgetMessageButtonAction(PreMenuText, GetMenuList())
                    });
            }
        }

        private List<List<Button>> GetMenuList()
        {
            var result = new List<List<Button>>();

            foreach (var menu in MenuBot.Keys)
            {
                result.Add(new List<Button>()
                    {new Button() {Text = menu}});
            }

            if (ChatOnWeb)
            {
                result.Add(new List<Button>()
                    {new Button() {Text = TalkToAgent}});
            }

            if (FbChecked)
            {
                result.Add(new List<Button>()
                    {new Button() {Icon = "fa-facebook",Link = $"http://m.me/{FbUserName}"}});
            }
            
            if (WhatsappChecked)
            {
                result.Add(new List<Button>()
                    {new Button() {Icon = "fa-whatsapp",Link = $"https://api.whatsapp.com/send?phone={WhatsappNumber}&text="}});
            }

            if (CallMeChecked)
            {
                result.Add(new List<Button>()
                    {new Button() {Icon = "fa-phone",Text = CallMeButtonText}});
            }

            return result;
        }
    }
}