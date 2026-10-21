using Newtonsoft.Json;
using RestSharp;

namespace Functions.Services
{
    public class PubnubService
    {
        public string Login(string username, string pass)
        {
            var client = new RestClient("https://admin.pubnub.com/api/");

            var request = new RestRequest("me", Method.POST, DataFormat.Json);
            request.AddJsonBody(new
            {
                email = username,
                password = pass
            });

            var response = client.Post(request);

            dynamic resultString = JsonConvert.DeserializeObject(response.Content);

            return resultString.result.token.Value.ToString();
        }

        public string GetState(string blockId, string sessionToken) //540819
        {
            var client = new RestClient("https://admin.pubnub.com/api/");

            var request = new RestRequest($"v1/blocks/key/{blockId}/block", Method.GET, DataFormat.Json);
            request.AddHeader("X-Session-Token", sessionToken);
            request.AddHeader("Accept", "application/json");
            var response = client.Get(request);

            dynamic resultString = JsonConvert.DeserializeObject(response.Content);

            return resultString.payload[0].state.ToString();
        }

        public string Stop(string keyId, string blockId, string sessionToken)
        {
            var client = new RestClient("https://admin.pubnub.com/api/");

            var request = new RestRequest($"/v1/blocks/key/{keyId}/block/{blockId}/stop", Method.GET, DataFormat.Json);
            request.AddHeader("X-Session-Token", sessionToken);
            request.AddHeader("Content-type", "application/json");
            var response = client.Post(request);

            dynamic resultString = JsonConvert.DeserializeObject(response.Content);

            return resultString.message.ToString();
        }

        public string Start(string keyId, string blockId, string sessionToken)
        {
            var client = new RestClient("https://admin.pubnub.com/api/");

            var request = new RestRequest($"/v1/blocks/key/{keyId}/block/{blockId}/start", Method.GET, DataFormat.Json);
            request.AddHeader("X-Session-Token", sessionToken);
            request.AddHeader("Content-type", "application/json");
            var response = client.Post(request);

            dynamic resultString = JsonConvert.DeserializeObject(response.Content);

            return resultString.message.ToString();
        }
    }


}
