using Mentor.bot.boundedContext.Services;
using Bot.telegram.funnel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public class FacebookUserInterfaceService : IFacebookUserInterfaceService
    {
        // TODO we might need storage::  private readonly IFacebookBotStorageService _facebookBotStorageService;

        public FacebookUserInterfaceService()
        {
           
        }
        public async Task<bool> SendMessage(JObject json, Dictionary<string, object> parameters = null)
        {
            var botId = parameters.GetBotId();
            var pageToken = parameters.GetPageToken();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }
    }
}
