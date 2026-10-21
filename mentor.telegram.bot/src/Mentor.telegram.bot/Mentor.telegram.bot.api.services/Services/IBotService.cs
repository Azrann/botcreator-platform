using Telegram.Bot;

namespace Mentor.telegram.bot.api.Services
{
    public interface IBotService
    {
        TelegramBotClient GetClient(string botId);
    }
}