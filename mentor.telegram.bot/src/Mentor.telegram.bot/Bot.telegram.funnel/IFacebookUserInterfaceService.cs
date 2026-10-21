using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.telegram.funnel
{
   public interface IFacebookUserInterfaceService
    {
        Task<bool> SendMessage(JObject json, Dictionary<string, object> parameters = null);

    }
}
