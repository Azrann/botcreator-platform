using System.Threading.Tasks;

namespace Mentor.telegram.bot.api.Services
{
    public interface IFbBotService
    {
        bool VerifySignature(string signature, string body, string botId);
        Task SendMessage(string originalMessage, string senderId,string botId);
    }
}
