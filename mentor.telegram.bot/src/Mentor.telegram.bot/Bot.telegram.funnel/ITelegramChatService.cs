using System;
using System.Threading.Tasks;

/*TODO: Techdebt here should not exists any reference to telegram or any other message implementation
 could be whatsapp etc. This is a generic funnel with generic actions.*/

namespace Bot.telegram.funnel
{
    public interface ITelegramChatService
    {
        Task SendMessageByUsername(string username, string message, Guid correlationId);
        Task SendImageByUsername(string username, string imageName, Guid correlationId, string message = null);
    }
}