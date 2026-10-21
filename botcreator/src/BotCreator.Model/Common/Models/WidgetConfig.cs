using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BotCreator.Core.Common.Models
{
    public class WidgetConfigBot
    {
        public string name { get; set; }
        public string message_bot_change { get; set; }
        public string url { get; set; }
        public string avarat_url { get; set; }
        public string id { get; set; }
    }

    public class WidgetConfigUser
    {
        public string avarat_url { get; set; }
    }

    public class WidgetConfigIcons
    {
    }

    public class WidgetConfigButtonsText
    {
        public string chat_now_btn { get; set; }
        public string register_btn { get; set; }
    }

    public class WidgetConfigFormFields
    {
        public string title { get; set; }
        public string placeholder { get; set; }
    }
    [JsonObject]
    public class WidgetConfigStyles
    {
        [JsonProperty("font-family")]
        public string fontFamily { get; set; }
        public string width { get; set; }
        public string right { get; set; }
        public string bottom { get; set; }
        public string position { get; set; }
    }
    [JsonObject]
    public class WidgetModalHeadStyle
    {
        [JsonProperty("background-color")]
        public string backgroundColor { get; set; }
    }
    [JsonObject]
    public class WidgetModalBodyStyle
    {
        [JsonProperty("background-color")]
        public string backgroundColor { get; set; }
    }
    [JsonObject]
    public class WidgetConfigButtonsStyle
    {
        [JsonProperty("font-family")]
        public string fontFamily { get; set; }
        [JsonProperty("background-color")]
        public string backgroundColor { get; set; }
        public string color { get; set; }
        [JsonProperty("min-height")]
        public string minHeight { get; set; }
        [JsonProperty("padding")]
        public string padding { get; set; }
        [JsonProperty("border-color")]
        public string borderColor { get; set; }
    }
    [JsonObject]
    public class WidgetConfigMessageStyle
    {
        [JsonProperty("box-shadow")]
        public string boxShadow { get; set; }
        [JsonProperty("background-color")]
        public string backgroundColor { get; set; }
        [JsonProperty("font-family")]
        public string fontFamily { get; set; }
    }

    [JsonObject]
    public class MessageBotChange
    {
        [JsonProperty("font-size")]
        public string fontSize { get; set; }
        public string margin { get; set; }
        [JsonProperty("border-top")]
        public string borderTop { get; set; }
        [JsonProperty("border-bottom")]
        public string borderBottom { get; set; }
    }
    [JsonObject]
    public class WidgetConfigInitialStyles
    {
        public WidgetConfigStyles widget { get; set; }
        public WidgetModalHeadStyle modal_head { get; set; }
        public WidgetModalBodyStyle modal_body { get; set; }
        public WidgetConfigButtonsStyle buttons { get; set; }
        public WidgetConfigMessageStyle message { get; set; }
        public MessageBotChange message_bot_change { get; set; }
    }
    [JsonObject]
    public class WidgetConfig
    {
        public object stylesheet { get; set; }
        public WidgetConfigBot bot { get; set; }
        public WidgetConfigUser user { get; set; }
        public string head_title { get; set; }
        public string first_message_text { get; set; }
        public string description { get; set; }
        public string icons { get; set; }
        public WidgetConfigButtonsText ButtonsText { get; set; }
        public object form { get; set; }
        public WidgetConfigInitialStyles initial_styles { get; set; }
        public bool Active { get; set; }
        public string WidgetUrl { get; set; }

        public static string WidgetUrlDefault = "https://botcreator.azureedge.net/javascript/helpWidget-0010h.js";

        public static WidgetConfig NewWidgetEn(string botId, string headerTextColor, string themeColor = "lightslategray", string secondaryColor = "black")
        {
             return new WidgetConfig()
             {
                 ButtonsText = new WidgetConfigButtonsText()
                 {
                     chat_now_btn = "Chat now",
                     register_btn = "Start"
                 },
                 description = "I am the Bot agent, I can assist with frequently asked questions & and issues",
                 first_message_text = "<b>Let me help you :)</b>",
                 head_title = "Chat with us!",
                 icons = "",
                 stylesheet = JObject.Parse($@"{{
                       '*' : {{
                          'line-height':'1.8'
                       }},
                      '.btn-simple:hover':{{  
                        'background-color':'{themeColor}',
                        'border-color':'{themeColor}',
                        'color': 'white'
                      }},
                     '.btn-simple':{{
                         'text-align':'center',
                         'font-size':'14px',
                         'min-height':'20px',
                         'line-height':'20px',
                         'background-color':'white',
                         'color':'black',
                         'border':'1px solid {themeColor}',
                         'margin':'2.5px!important',
                         'border-radius':'5px',
                         'text-decoration':'none',
                         'padding-top':'8px',
                         'padding-bottom':'8px',
                         'padding-left':'11px',
                         'padding-right':'11px'
                     }},

                        '.helpWidget_person_message':{{  
                              'box-sizing':'border-box',
                              'text-align':'left',
                              'background-color':'#dddddd',
                              'box-shadow':'none',
                              'border-radius':'5px',
                              'padding':'0',
                              'margin':'0.5em 0.25em 0.5em',
                              'word-break':'break-word',
                              'font-size: ':'14px',
                              'color':'black',
                              'font-weight':'400',
                              'font-style':'normal',
                              'width':'fit-content',
                              'font-family':'Open Sans'
                           }},
                        '.helpWidget_person[data-bot=false] .helpWidget_person_message':{{  
                              'background-color':'{themeColor}',
                              'color':'white'
                        }},
                         '.helpWidget_person_avatar':{{  
                              'display':'none'
                           }},
                        '.helpWidget_person[data-bot=true] .helpWidget_person_avatar':{{  
                              'display':'block'
                        }},
                         '.helpWidget_person_message.btn_wrapper':{{  
                              'background-color':'transparent'
                         }},
                         '.personal_credentials':{{  
                              'display':'none'
                         }},
                        '.helpWidget_person_avatar':{{  
	                        'display':'none'
                           }},
                         '.helpWidget_feedback_buttons':{{  
                              'display':'none'
                           }},
                        '.helpWidget_custom-button':{{  
	                        'font-size':'15px'
                           }},
                        '.icon-link i' : {{
                            'font-size':'24px'
                            }},
                        'textarea' : {{
                            'border':'1px solid {themeColor}'
                            }},
                         '.helpWidget_clear_message a': {{
                              'color':'{secondaryColor}',
                              'font-weight':'bold',
                              'text-decoration':'none'
                           }},
                           '.helpWidget_clear_message a:visited, .helpWidget_clear_message a:focus, .helpWidget_clear_message a:active' :  {{
                              'text-decoration':'none'
                           }},
                           '.helpWidget_clear_message a:hover' :  {{
                              'color':'{themeColor}'
                           }},
                           '.helpWidget_modal_head' :  {{
                              'color':'{headerTextColor}'
                           }}
                    }}".Replace("\r\n", string.Empty)),
                 user = new WidgetConfigUser()
                 {
                     avarat_url = "https://botcreator.azureedge.net/images/avatar.jpg",
                 },
                 bot = new WidgetConfigBot()
                 {
                     avarat_url = "https://botcreator.azureedge.net/images/toy-robot-small.jpg",
                     id = botId,
                     name = "Bot Agent",
                     message_bot_change = "The BOT was changed",
                     url= "https://botcreator.online"
                 },
                 form = JObject.Parse(@"{
                       'name' : {
                          'title':'Your name:',
                          'placeholder':'Type your name...'
                       },
                      'email':{  
                          'title':'E-mail:',
                          'placeholder':'Type your email...'
                      }
                    }".Replace("\r\n", string.Empty)),
                 initial_styles = new WidgetConfigInitialStyles()
                 {
                     message_bot_change = new MessageBotChange()
                     {
                         borderBottom = "1px dotted",
                         borderTop = "1px dotted",
                         fontSize = "12px",
                         margin = "5px 0"
                     },
                     buttons = new WidgetConfigButtonsStyle()
                     {
                         backgroundColor = themeColor,
                         fontFamily = "Open Sans",
                         color = "white",
                         minHeight = "45px",
                         padding = "8px 11px",
                         borderColor = themeColor,
                     },
                     message = new WidgetConfigMessageStyle()
                     {
                         fontFamily = "Open Sans",
                         backgroundColor = "#f5f5f5",
                         boxShadow = "none"
                     },
                     modal_body = new WidgetModalBodyStyle()
                     {
                         backgroundColor = "white"
                     },
                     modal_head = new WidgetModalHeadStyle()
                     {
                         backgroundColor = themeColor
                     },
                     widget = new WidgetConfigStyles()
                     {
                         fontFamily = "Open Sans",
                         width = "350px",
                         right = "1em",
                         bottom = "1em",
                         position = "fixed"
                     }
                     
                 }
             };
        }

        public static WidgetConfig NewWidgetEs(string botId, string headerTextColor, string primaryColor = "lightslategray", string secondaryColor = "black")
        {
            return new WidgetConfig()
            {
                RemoveMessages = false,
                ButtonsText = new WidgetConfigButtonsText()
                {
                    chat_now_btn = "Hablar ahora",
                    register_btn = "Comenzar"
                },
                description = "Si necesitas algo te puedo ayudar con las preguntas mas frecuentes",
                first_message_text = "<b>Pincha aqui para saber mas :)</b>",
                head_title = "Habla con nosotros!",
                icons = "",
                stylesheet = JObject.Parse($@"{{
                       '*' : {{
                          'line-height':'1.8'
                       }},
                      '.btn-simple:hover':{{  
                        'background-color':'{primaryColor}',
                        'border-color':'{primaryColor}',
                        'color': 'white'
                      }},
                     '.btn-simple':{{
                         'text-align':'center',
                         'font-size':'14px',
                         'min-height':'20px',
                         'line-height':'20px',
                         'background-color':'white',
                         'color':'black',
                         'border':'1px solid {primaryColor}',
                         'margin':'2.5px!important',
                         'border-radius':'5px',
                         'text-decoration':'none',
                         'padding-top':'8px',
                         'padding-bottom':'8px',
                         'padding-left':'11px',
                         'padding-right':'11px'
                     }},

                        '.helpWidget_person_message':{{  
                              'box-sizing':'border-box',
                              'text-align':'left',
                              'background-color':'#dddddd',
                              'box-shadow':'none',
                              'border-radius':'5px',
                              'padding':'0',
                              'margin':'0.5em 0.25em 0.5em',
                              'word-break':'break-word',
                              'font-size: ':'14px',
                              'color':'black',
                              'font-weight':'400',
                              'font-style':'normal',
                              'width':'fit-content',
                              'font-family':'Open Sans'
                           }},
                        '.helpWidget_person[data-bot=false] .helpWidget_person_message':{{  
                              'background-color':'{primaryColor}',
                              'color':'white'
                        }},
                         '.helpWidget_person_avatar':{{  
                              'display':'none'
                           }},
                         '.helpWidget_feedback_buttons':{{  
                              'display':'none'
                           }},
                        '.helpWidget_person[data-bot=true] .helpWidget_person_avatar':{{  
                              'display':'block'
                        }},
                         '.helpWidget_person_message.btn_wrapper':{{  
                              'background-color':'transparent'
                         }},
                         '.personal_credentials':{{  
                              'display':'none'
                         }},
                        '.helpWidget_person_avatar':{{  
	                        'display':'none'
                           }},
                        '.helpWidget_custom-button':{{  
	                        'font-size':'15px'
                           }},
                        '.icon-link i' : {{
                            'font-size':'24px'
                            }},
                        'textarea' : {{
                            'border':'1px solid {primaryColor}'
                            }},
                         '.helpWidget_clear_message a' :  {{
                              'color':'{secondaryColor}',
                              'font-weight':'bold',
                              'text-decoration':'underline'
                           }},
                           '.helpWidget_clear_message a:visited, .helpWidget_clear_message a:focus, .helpWidget_clear_message a:active' : {{
                              'text-decoration':'none'
                           }},
                           '.helpWidget_clear_message a:hover' :  {{
                              'color':'{primaryColor}'
                           }},
                           '.helpWidget_modal_head' :  {{
                              'color':'{headerTextColor}'
                           }}

                    }}".Replace("\r\n", string.Empty)),
                user = new WidgetConfigUser()
                {
                    avarat_url = "https://botcreator.azureedge.net/images/avatar.jpg",
                },
                bot = new WidgetConfigBot()
                {
                    avarat_url = "https://botcreator.azureedge.net/images/toy-robot-small.jpg",
                    id = botId,
                    name = "Bot Yoga",
                    message_bot_change = "Ahora hablas con",
                    url = ""
                },
                form =
                    JObject.Parse(@"{
                       'name' : {
                          'title':'Nombre:',
                          'placeholder':'Escribe tu nombre...'
                       },
                      'email':{  
                          'title':'Email:',
                          'placeholder':'Escribe tu email...'
                      }
                    }".Replace("\r\n", string.Empty)),
                initial_styles = new WidgetConfigInitialStyles()
                {
                    message_bot_change = new MessageBotChange()
                    {
                        borderBottom = "1px dotted",
                        borderTop = "1px dotted",
                        fontSize = "12px",
                        margin = "5px 0"
                    },
                    buttons = new WidgetConfigButtonsStyle()
                    {
                        backgroundColor = primaryColor,
                        fontFamily = "Open Sans",
                        color = "white",
                        minHeight = "45px",
                        padding = "8px 11px",
                        borderColor = primaryColor,
                    },
                    message = new WidgetConfigMessageStyle()
                    {
                        fontFamily = "Open Sans",
                        backgroundColor = "#f5f5f5",
                        boxShadow = "none"
                    },
                    modal_body = new WidgetModalBodyStyle()
                    {
                        backgroundColor = "white"
                    },
                    modal_head = new WidgetModalHeadStyle()
                    {
                        backgroundColor = primaryColor
                    },
                    widget = new WidgetConfigStyles()
                    {
                        fontFamily = "Open Sans",
                        width = "350px",
                        right = "1em",
                        bottom = "1em",
                        position = "fixed"
                    }

                }
            };
        }

        public bool RemoveMessages { get; set; }

        public static WidgetConfig NewWidget(WidgetConfigData widgetConfigData, string botId, bool isDevEnv)
        {
            if (widgetConfigData == null)
            {
                return WidgetConfig.NewWidgetEs(botId,"black");
            }

            var headerTextColor = "black";
            if (!string.IsNullOrEmpty(widgetConfigData.HeaderTextColor))
            {
                headerTextColor = widgetConfigData.HeaderTextColor;
            }

            WidgetConfig config;
            if (widgetConfigData.Language != null && widgetConfigData.Language == "es")
            {
                config = WidgetConfig.NewWidgetEs(widgetConfigData.BotId, headerTextColor,
                    widgetConfigData.PrimaryColor,
                    widgetConfigData.SecondaryColor);
            }
            else
            {
                config = WidgetConfig.NewWidgetEn(widgetConfigData.BotId, headerTextColor,
                    widgetConfigData.PrimaryColor,
                    widgetConfigData.SecondaryColor);
            }

            config.ButtonsText.chat_now_btn = widgetConfigData.ChatNow;
            config.first_message_text = widgetConfigData.BubbleMessage;
            config.description = widgetConfigData.Description;
            config.bot.name = widgetConfigData.BotName;
            config.RemoveMessages = false;
            config.Active = widgetConfigData.Active;
            
            if (widgetConfigData.BotAvatarUrl != null)
            {
                config.bot.avarat_url = widgetConfigData.BotAvatarUrl;
            }

            config.WidgetUrl = string.IsNullOrEmpty(widgetConfigData.WidgetUrl) ? WidgetUrlDefault : widgetConfigData.WidgetUrl;
            config.RemoveMessages = widgetConfigData.RemovePrevious;

            if (isDevEnv)
            {
                config.WidgetUrl = config.WidgetUrl.Replace(".js", "test.js");
            }
            return config;
        }
    }
}
