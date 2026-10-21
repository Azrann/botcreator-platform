using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Bot.telegram.funnel;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public class TelegramUserInterfaceService : IUserInterfaceService
    {
        private readonly IBotService _botService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TelegramUserInterfaceService));

        public TelegramUserInterfaceService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task<bool> RemovePreviousMessage(Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();
            var messageId = (int) parameters.GetMessageId();
            try
            {
                await _botService.GetClient(botId).DeleteMessageAsync(chatId, messageId);
            }
            catch (Exception ex)
            {
                Log.LogErr($"TelegramUserInterfaceService RemovePreviousMessage error {ex.Message}", parameters.GetCorrelationId());
                return false;
            }

            return true;
        }

        public async Task<bool> SendMessage(string text, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();
            try
            {
                await _botService.GetClient(botId).SendTextMessageAsync(
                    chatId,
                    text,
                    parseMode: ParseMode.Html,
                    replyMarkup: new ReplyKeyboardRemove());
            }
            catch (Exception e)
            {
                Log.LogErr($"TelegramUserInterfaceService SendMessage error {e.Message}", parameters.GetCorrelationId());
                return false;
            }

            return true;
        }

        public async Task<bool> SendMessageWithButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();
            var inlineKeyboard = new InlineKeyboardMarkup(buttons.Select(y => y.Select(x => InlineKeyboardButton.WithCallbackData(x))));

            await _botService.GetClient(botId).SendTextMessageAsync(
                chatId,
                text,
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboard);
            return true;
        }

        public async Task<bool> SendInputMessage(string text, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();
            
            await _botService.GetClient(botId).SendTextMessageAsync(
                chatId,
                text,
                parseMode: ParseMode.Html,
                replyMarkup: new ForceReplyMarkup() { Selective = true });

            return true;
        }

        public async Task<bool> SendMessageWithMenuButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();

            var keyboardMarkup = new ReplyKeyboardMarkup
            {
                Keyboard = buttons.Select(x => x.Select(y => new KeyboardButton(y)).ToArray()).ToArray()
            };

            await _botService.GetClient(botId).SendTextMessageAsync(
                chatId,
                text,
                parseMode: ParseMode.Html,
                replyMarkup: keyboardMarkup);
            return true;
        }

        public async Task<bool> SendMessageWithMenuButtonsAdvance(string text, List<List<Button>> buttons, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();

            var keyboardMarkup = new ReplyKeyboardMarkup
            {
                Keyboard = buttons.Select(x => x.Select(y => new KeyboardButton(y.Text)).ToArray()).ToArray()
            };

            await _botService.GetClient(botId).SendTextMessageAsync(
                chatId,
                text,
                parseMode: ParseMode.Html,
                replyMarkup: keyboardMarkup);

            return true;
        }

        public async Task<bool> SendImage(string fileName, Dictionary<string, object> parameters, bool removeKeyboard = true)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatId();
            if (removeKeyboard)
            {
                await _botService.GetClient(botId).SendPhotoAsync(
                    chatId,
                    new InputOnlineFile(new MemoryStream(System.IO.File.ReadAllBytes($"wwwroot\\Menu\\{fileName}")), fileName),
                    replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                await _botService.GetClient(botId).SendPhotoAsync(
                    chatId,
                    new InputOnlineFile(new MemoryStream(System.IO.File.ReadAllBytes($"wwwroot\\Menu\\{fileName}")), fileName));
            }
            return true;
        }

        public async Task<bool> SendMessageCommand(string text, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var chatId = parameters.GetChatIdCommand();
            Log.LogInf($"TelegramUserInterfaceService SendMessageCommand to chat {chatId}", parameters.GetCorrelationId());
            try
            {
                await _botService.GetClient(botId).SendTextMessageAsync(
                    chatId,
                    text,
                    parseMode: ParseMode.Html,
                    replyMarkup: new ReplyKeyboardRemove());
            }
            catch (Exception e)
            {
                Log.LogErr($"TelegramUserInterfaceService SendMessageCommand error {e.Message}", parameters.GetCorrelationId());
                return false;
            }

            return true;
        }

        public async Task<bool> SendMessageCommandByUserId(string text, long chatId, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            try
            {
                await _botService.GetClient(botId).SendTextMessageAsync(
                    chatId,
                    text,
                    parseMode: ParseMode.Html,
                    replyMarkup: new ReplyKeyboardRemove());
            }
            catch (Exception e)
            {
                Log.LogErr($"TelegramUserInterfaceService SendMessageCommandByUserId error {e.Message} {JsonConvertExt.SerializeObject(e)}", parameters.GetCorrelationId());
                return false;
            }

            return true;
        }
    }
}
