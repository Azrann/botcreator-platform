using Mentor.bot.boundedContext.Services;
using Bot.telegram.funnel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.telegram.bot.api.Services
{
    public class FbBotService : IFbBotService
    {
        private readonly IFacebookBotStorageService _facebookBotStorageService;
        private readonly IFacebookUserInterfaceService _facebookUserInterfaceService;
        private static readonly char[] HexLookup = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public FbBotService(IFacebookBotStorageService facebookBotStorageService, IFacebookUserInterfaceService facebookUserInterfaceService)
        {
            _facebookBotStorageService = facebookBotStorageService;
            _facebookUserInterfaceService = facebookUserInterfaceService;
        }
        public Task SendMessage(string originalMessage, string senderId, string botId)
        {
            var replyMessage = "FB bot replied: " + originalMessage;
            var replyMsgJson = GetMessageTemplate(replyMessage, senderId);
            var pageToken = _facebookBotStorageService.GetFacebookBotDataById(botId).PageToken;
            
            var sendMsgParams = new Dictionary<string, object>();
            sendMsgParams.AddBotId(botId);
            sendMsgParams.AddPageToken(pageToken);
            //Is this the correct place to call the service method?
            return _facebookUserInterfaceService.SendMessage(replyMsgJson, sendMsgParams);
        }
        public bool VerifySignature(string signature, string body, string botId)
        {
            var appSecret = _facebookBotStorageService.GetFacebookBotDataById(botId).AppSecret;
            signature = signature.Replace("sha1=", "");
            string hashedValue = ComputeHash(appSecret, body);
            if (hashedValue.Equals(signature, StringComparison.OrdinalIgnoreCase))
                return true;
            else return false;
        }

        public JObject GetMessageTemplate(string text, string sender)
        {
            return JObject.FromObject(new
            {
                recipient = new { id = sender },
                message = new {text }
            });
        }
        private string ComputeHash(string secretKey, string textToHash)
        {
            byte[] secret = Encoding.UTF8.GetBytes(secretKey);
            var hasher = new HMACSHA1(secret);

            byte[] textBytes = Encoding.UTF8.GetBytes(textToHash);

            return ToHex(hasher.ComputeHash(textBytes));
        }

        /// <summary>
        /// Converts a <see cref="T:byte[]"/> to a hex-encoded string.
        /// </summary>
        private string ToHex(byte[] data)
        {
            if (data == null)
            {
                return string.Empty;
            }

            char[] content = new char[data.Length * 2];
            int output = 0;
            byte d;
            for (int input = 0; input < data.Length; input++)
            {
                d = data[input];
                content[output++] = HexLookup[d / 0x10];
                content[output++] = HexLookup[d % 0x10];
            }
            return new string(content);
        }


    }
}

