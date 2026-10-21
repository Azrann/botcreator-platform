using System;
using System.Collections.Generic;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot.telegram.funnel
{

    public class FunnelConverter : JsonConverter
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
            var funnel = (Funnel) value;
            writer.WriteStartObject();
            writer.WritePropertyName("$type");
            writer.WriteValue(value.GetType().Name);
            writer.WritePropertyName("CallbackActions");
            if (funnel.CallbackActions == null || funnel.CallbackActions.Count == 0)
            {
                writer.WriteValue("{}");
            }
            else
            {
                WriteDictionaryJson(writer, value, serializer, funnel.CallbackActions);
            }
            writer.WritePropertyName("MessageActions");
            if (funnel.MessageActions == null || funnel.MessageActions.Count == 0)
            {
                writer.WriteValue("{}");
            }
            else
            {
                WriteDictionaryJson(writer, value, serializer, funnel.MessageActions);
            }
            writer.WritePropertyName("EventActions");
            if (funnel.EventActions == null || funnel.EventActions.Count == 0)
            {
                writer.WriteValue("{}");
            }
            else
            {
                WriteDictionaryJson(writer, value, serializer, funnel.EventActions);
            }
            writer.WriteEndObject();

        }

        private void WriteDictionaryJson(JsonWriter writer, object value, JsonSerializer serializer, Dictionary<ITrigger, List<FunnelAction>> funnelCallbackActions)
        {
            writer.WriteStartArray();
            foreach (var funnelCallbackAction in funnelCallbackActions)
            {
                writer.WriteStartObject();
                CommonConverter.WriteTrigger(writer, serializer, funnelCallbackAction.Key);
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var action in funnelCallbackAction.Value)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("$type");
                    writer.WriteValue(action.GetType().Name);
                    writer.WritePropertyName("Value");
                    serializer.Serialize(writer, action);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        
        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            Funnel funnel = null;
            switch (jsonObject["$type"].Value<string>())
            {
                case "Funnel":
                    funnel = new Funnel()
                    {
                        MessageActions = new Dictionary<ITrigger, List<FunnelAction>>(),
                        CallbackActions = new Dictionary<ITrigger, List<FunnelAction>>(),
                        EventActions = new Dictionary<ITrigger, List<FunnelAction>>()
                    };
                    break;
            }
            
            if (jsonObject["CallbackActions"].HasValues)
            {
                ReadDictionaryJson(reader, objectType, existingValue, serializer,funnel.CallbackActions,
                    jsonObject["CallbackActions"]);
            }

            if (jsonObject["MessageActions"].HasValues)
            {
                ReadDictionaryJson(reader, objectType, existingValue, serializer, funnel.MessageActions,
                    jsonObject["MessageActions"]);
            }

            if (jsonObject["EventActions"].HasValues)
            {
                ReadDictionaryJson(reader, objectType, existingValue, serializer, funnel.EventActions,
                    jsonObject["EventActions"]);
            }
            return funnel;
        }

        private void ReadDictionaryJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer, Dictionary<ITrigger, List<FunnelAction>> funnelCallbackActions, JToken jToken)
        {
            foreach (var keyValuePairJson in jToken.Children())
            {
                var key = default(ITrigger);
                key = CommonConverter.DeserializeTrigger(serializer, keyValuePairJson);
                var actions = new List<FunnelAction>();
                CommonConverter.ReadActionList(serializer, keyValuePairJson["Value"], actions);
                funnelCallbackActions.Add(key,actions);
            }
        }

        
    }
}
