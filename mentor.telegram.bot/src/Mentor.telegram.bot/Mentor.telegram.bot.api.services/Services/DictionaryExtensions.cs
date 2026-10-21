using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentor.telegram.bot.api.Services
{
    public static class DictionaryExtensions
    {
        public static decimal GetDecimal(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? decimal.Parse(dictionary[key]) : 0.0m;
        }

        public static double GetDouble(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? double.Parse(dictionary[key]) : 0;
        }

        public static DateTime? GetDateTime(this Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.ContainsKey(key) && !string.IsNullOrEmpty(dictionary[key]))
            {
                return DateTime.Parse(dictionary[key]);
            }

            return null;
        }

        public static string GetString(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : string.Empty;
        }

        public static int GetInt(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? int.Parse(dictionary[key]) : 0;
        }

        public static long GetLong(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? long.Parse(dictionary[key]) : 0;
        }

        public static decimal GetDecimalFromDouble(this Dictionary<string, string> dictionary, string key)
        {
            return dictionary.ContainsKey(key) && !string.IsNullOrEmpty(dictionary[key]) ? 
                Convert.ToDecimal(double.Parse(dictionary[key])) : 0.0m;
        }

        public static IEnumerable<long> GetListOfLongs(this Dictionary<string, string> dictionary, string key)
        {
            IEnumerable<long> result = new List<long>();

            if (dictionary.ContainsKey(key) && !string.IsNullOrEmpty(dictionary[key]))
            {
                result = dictionary[key].Split(',').Select(long.Parse);
            }

            return result;
        }
    }
}