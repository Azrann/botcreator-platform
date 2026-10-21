using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mentor.telegram.coinpayments.Helpers
{
    public static class PaymentHelper
    {
        public static List<KeyValuePair<string, string>> ToKeyValuePair(this object obj)
        {
            List<KeyValuePair<string, string>> ksv = new List<KeyValuePair<string, string>>();

            foreach (var item in obj.GetType().GetProperties())
            {
                string name = string.Empty;
                var atm = item.GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false).FirstOrDefault();
                if (atm != null)
                {
                    name = ((Newtonsoft.Json.JsonPropertyAttribute)atm).PropertyName;
                }
                else name = item.Name;

                if (item.GetValue(obj) != null) ksv.Add(new KeyValuePair<string, string>(name, item.GetValue(obj).ToString()));
            }
            return ksv;
        }

        public static Dictionary<string, string> ToKeyValuePairDictionary(this object obj)
        {
            Dictionary<string, string> ksv = new Dictionary<string, string>();

            foreach (var item in obj.GetType().GetProperties())
            {
                string name = string.Empty;
                var atm = item.GetCustomAttributes(typeof(Newtonsoft.Json.JsonPropertyAttribute), false).FirstOrDefault();
                if (atm != null)
                {
                    name = ((Newtonsoft.Json.JsonPropertyAttribute)atm).PropertyName;
                }
                else name = item.Name;

                if (item.GetValue(obj) != null) ksv.Add(name, item.GetValue(obj).ToString());
            }
            return ksv;
        }

        public static string GetUrl(this string mainUrl, List<KeyValuePair<string, string>> obj)
        {
            var i = 0;
            foreach (var item in obj)
            {
                if (i == 0) mainUrl += $"{item.Key}={item.Value}";
                else mainUrl += $"&{item.Key}={item.Value}";
                i += 1;
            }
            return mainUrl;
        }

        public static bool Validate(string sharedSecretKey, Dictionary<string, string> keyValues, string hmacHeader)
        {
            var body = GetKeyValues(keyValues);
            
            var createSignature = CreateSignature(sharedSecretKey, body);

            return createSignature ==  hmacHeader;
        }

        public static string CreateSignature(string sharedSecretKey, string body)
        {
            var keyBytes = Encoding.UTF8.GetBytes(sharedSecretKey);
            var dataBytes = Encoding.UTF8.GetBytes(body);

            var hmac = new HMACSHA512(keyBytes);
            var hmacBytes = hmac.ComputeHash(dataBytes);

            var signature = BitConverter.ToString(hmacBytes);
            return signature.Replace("-", "").ToLower();
        }

        public static string GetKeyValues(Dictionary<string, string> keyValues)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var collectionKey in keyValues.Keys.OrderBy(x => x))
            {
                if (first)
                {
                    // TODO WebUtility.UrlEncode can also work?
                    sb.Append($"{Uri.EscapeDataString(collectionKey)}={Uri.EscapeDataString(keyValues[collectionKey])}");
                    first = false;
                }
                else
                {
                    sb.Append($"&{Uri.EscapeDataString(collectionKey)}={Uri.EscapeDataString(keyValues[collectionKey])}");
                }
            }

            return sb.ToString().Replace("%20", "+");
        }
    }
}
