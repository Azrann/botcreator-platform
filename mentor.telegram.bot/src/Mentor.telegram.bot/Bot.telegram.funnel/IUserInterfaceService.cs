using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.telegram.funnel
{
   

    public interface IUserInterfaceService
    {
        Task<bool> RemovePreviousMessage(Dictionary<string, object> parameters = null);
        Task<bool> SendMessage(string text, Dictionary<string, object> parameters = null);
        Task<bool> SendMessageWithButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters = null);
        Task<bool> SendInputMessage(string text, Dictionary<string, object> parameters = null);
        Task<bool> SendMessageWithMenuButtons(string text, List<List<string>> buttons, Dictionary<string, object> parameters);
        Task<bool> SendMessageWithMenuButtonsAdvance(string text, List<List<Button>> buttons, Dictionary<string, object> parameters);
        Task<bool> SendImage(string fileName, Dictionary<string, object> parameters, bool removeKeyboard = true);
        Task<bool> SendMessageCommand(string text, Dictionary<string, object> parameters = null);
        Task<bool> SendMessageCommandByUserId(string text, long chatId, Dictionary<string, object> parameters = null);
    }
}
