using System;
using System.Collections.Generic;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot.telegram.funnel
{
    public class BotConverter : JsonConverter
    {
        public override bool CanWrite => true;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IFunnel);
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            var bot = (Bot) value;
            writer.WriteStartObject();
            writer.WritePropertyName("$type");
            writer.WriteValue(value.GetType().Name);
            if (bot.Id != null)
            {
                writer.WritePropertyName("Id");
                writer.WriteValue(bot.Id);
            }
            writer.WritePropertyName("adminId");
            writer.WriteValue(bot.AdminId);
            writer.WritePropertyName("TriggersToFunnel");
            if (bot.TriggersToFunnel == null || bot.TriggersToFunnel.Count == 0)
            {
                writer.WriteValue("{}");
            }
            else
            {
                WriteDictionaryJson(writer, value, serializer, bot.TriggersToFunnel);
            }
            writer.WriteEndObject();

        }

        private void WriteDictionaryJson(JsonWriter writer, object value, JsonSerializer serializer, Dictionary<ITrigger, Guid> triggersToFunnel)
        {
            writer.WriteStartArray();
            foreach (var triggerToFunnel in triggersToFunnel)
            {
                writer.WriteStartObject();
                CommonConverter.WriteTrigger(writer, serializer, triggerToFunnel.Key);
                writer.WritePropertyName("Value");
                writer.WriteValue(triggerToFunnel.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        
        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            Bot bot = null;
            switch (jsonObject["$type"].Value<string>())
            {
                case "Bot":
                    bot = new Bot()
                    {
                        TriggersToFunnel = new Dictionary<ITrigger, Guid>()
                    };
                    break;
            }
            
            if (jsonObject.ContainsKey("Id"))
            {
                bot.Id = jsonObject["Id"].ToString();
            }

            if (jsonObject.ContainsKey("adminId"))
            {
                bot.AdminId = new Guid(jsonObject["adminId"].ToString());
            }

            if (jsonObject["TriggersToFunnel"].HasValues)
            {
                ReadDictionaryJson(reader, objectType, existingValue, serializer, bot.TriggersToFunnel,
                    jsonObject["TriggersToFunnel"]);
            }
            return bot;
        }

        private void ReadDictionaryJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer, Dictionary<ITrigger, Guid> triggersToFunnel, JToken jToken)
        {
            foreach (var keyValuePairJson in jToken.Children())
            {
                var key = default(ITrigger);
                key = CommonConverter.DeserializeTrigger(serializer, keyValuePairJson);
                triggersToFunnel.Add(key, new Guid(keyValuePairJson["Value"].ToString()));
            }
        }

        
    }
}
