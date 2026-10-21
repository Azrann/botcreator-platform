using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.telegram.funnel;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public class WidgetUserInterfaceService : IUserInterfaceService
    {
        private readonly IPubnubMessageBus _pubnubMessageBus;

        public WidgetUserInterfaceService(IPubnubMessageBus pubnubMessageBus)
        {
            _pubnubMessageBus = pubnubMessageBus;
        }

        public Task<bool> RemovePreviousMessage(Dictionary<string, object> parameters = null)
        {
            return Task.FromResult(true);
        }

        public Task<bool> SendMessage(string text, Dictionary<string, object> parameters = null)
        {
            var message = new
            {
                text,
                from = "Bot"
            };

            _pubnubMessageBus.Start(parameters.GetCorrelationId());
            _pubnubMessageBus.Publish(message, parameters.GetChatId().ToString());

            return Task.FromResult(true);
        }

        public Task<bool> SendMessageWithButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters = null)
        {
            var message = new
            {
                text,
                buttons,
                from = "Bot"
            };

            _pubnubMessageBus.Start(parameters.GetCorrelationId());
            _pubnubMessageBus.Publish(message, parameters.GetChatId().ToString());

            return Task.FromResult(true);
        }

        public Task<bool> SendMessageWithMenuButtonsAdvance(string text, List<List<Button>> buttons, Dictionary<string, object> parameters)
        {
            var message = new
            {
                text,
                buttons,
                from = "Bot"
            };

            _pubnubMessageBus.Start(parameters.GetCorrelationId());
            _pubnubMessageBus.Publish(message, parameters.GetChatId().ToString());

            return Task.FromResult(true);
        }

        public Task<bool> SendInputMessage(string text, Dictionary<string, object> parameters = null)
        {
            if (parameters.ContainsKey("channelId"))
            {
                Console.WriteLine($"{parameters["channelId"]} : {text}");
            }
            else
            {
                Console.WriteLine(text);
                Console.WriteLine($"Reply to:{text}");
            }

            return Task.FromResult(true);
        }

        public Task<bool> SendMessageWithMenuButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters)
        {
            Console.WriteLine(text);
            Console.WriteLine();
            foreach (var row in buttons)
            {
                foreach (var buttonText in row)
                {
                    Console.Write($"<{buttonText}>");
                }
                Console.WriteLine();
            }

            return Task.FromResult(true);
        }


        public Task<bool> SendImage(string fileName, Dictionary<string, object> parameters, bool removeKeyboard = true)
        {
            if (parameters.ContainsKey("channelId"))
            {
                Console.WriteLine($"{parameters["channelId"]} : ShowingImage {fileName}");
            }
            else
            {
                Console.WriteLine($" ShowingImage {fileName}");
            }

            return Task.FromResult(true);
        }

        public Task<bool> RemovePreviousMessageCommand(Dictionary<string, object> parameters)
        {
            Console.Clear();
            return Task.FromResult(true);
        }

        public Task<bool> SendMessageCommand(string text, Dictionary<string, object> parameters = null)
        {
            var message = new
            {
                Command=  text,
                from = "Bot"
            };

            _pubnubMessageBus.Start(parameters.GetCorrelationId());
            _pubnubMessageBus.Publish(message, parameters.GetChatId().ToString());

            return Task.FromResult(true);
        }

        public Task<bool> SendMessageCommandByUserId(string text, long chatId, Dictionary<string, object> parameters = null)
        {
            if (parameters.ContainsKey("channelId"))
            {
                Console.WriteLine($"{parameters["channelId"]} : {text}");
            }
            else
            {
                Console.WriteLine(text);
            }

            return Task.FromResult(true);
        }
    }
}
