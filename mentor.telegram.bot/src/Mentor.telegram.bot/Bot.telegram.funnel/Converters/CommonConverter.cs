using System;
using System.Collections.Generic;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot.telegram.funnel
{
    public static class CommonConverter
    {
        public static void WriteTrigger(JsonWriter writer, JsonSerializer serializer, ITrigger trigger)
        {
            var triggerType = trigger.GetType().Name;
            writer.WritePropertyName("$type");
            writer.WriteValue(triggerType);
            writer.WritePropertyName("Priority");
            writer.WriteValue(trigger.Priority);
            writer.WritePropertyName("Key");
            if (triggerType == "AndTrigger")
            {
                WriteAndTrigger(writer, serializer, trigger);
            }
            else if (triggerType == "OrTrigger")
            {
                WriteOrTrigger(writer, serializer, trigger);
            }
            else
            {
                serializer.Serialize(writer, trigger);
            }
        }

        public static void WriteAndTrigger(JsonWriter writer, JsonSerializer serializer, ITrigger trigger)
        {
            writer.WriteStartArray();
            foreach (var childTrigger in ((AndTrigger)trigger).Triggers)
            {
                writer.WriteStartObject();
                WriteTrigger(writer, serializer, childTrigger);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        public static void WriteOrTrigger(JsonWriter writer, JsonSerializer serializer, ITrigger trigger)
        {
            writer.WriteStartArray();
            foreach (var childTrigger in ((OrTrigger)trigger).Triggers)
            {
                writer.WriteStartObject();
                WriteTrigger(writer, serializer, childTrigger);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        public static ITrigger DeserializeTrigger(JsonSerializer serializer, JToken triggerToken)
        {
            ITrigger key;
            string triggerType = triggerToken["$type"].Value<string>();

            switch (triggerType)
            {
                case "MessageTrigger":
                    key = new MessageTrigger(string.Empty);
                    break;
                case "MessageIsNotTrigger":
                    key = new MessageIsNotTrigger(null);
                    break;
                case "PropertiesTrigger":
                    key = new PropertiesTrigger(new Dictionary<string, object>());
                    break;
                case "PropertiesStartWithTrigger":
                    key = new PropertiesStartWithTrigger(new Dictionary<string, object>());
                    break;
                case "TagsAddedTrigger":
                    key = new TagsAddedTrigger(new List<string>());
                    break;
                case "TagsNotAddedTrigger":
                    key = new TagsNotAddedTrigger(new List<string>());
                    break;
                case "TagAddedTrigger":
                    key = new TagAddedTrigger(string.Empty);
                    break;
                case "TagNotAddedTrigger":
                    key = new TagNotAddedTrigger(string.Empty);
                    break;
                case "ReplyMessageTrigger":
                    key = new ReplyMessageTrigger(string.Empty);
                    break;
                case "AndTrigger":
                    key = new AndTrigger();
                    key.Priority = triggerToken["Priority"].Value<int>();
                    return ReadComplexAndTrigger(key, triggerToken["Key"], serializer);
                case "OrTrigger":
                    key = new OrTrigger();
                    key.Priority = triggerToken["Priority"].Value<int>();
                    return ReadComplexOrTrigger(key, triggerToken["Key"], serializer);
                case "HasClientCreatedTagTrigger":
                    key = new HasClientCreatedTagTrigger();
                    break;
                case "HasNotClientCreatedTagTrigger":
                    key = new HasNotClientCreatedTagTrigger();
                    break;
                case "HasEmailTagTrigger":
                    key = new HasEmailTagTrigger();
                    break;
                case "HasNotEmailTagTrigger":
                    key = new HasNotEmailTagTrigger();
                    break;
                case "TagBiggerThan0Trigger":
                    key = new TagBiggerThan0Trigger(string.Empty);
                    break;
                case "TagNotBiggerThan0Trigger":
                    key = new TagNotBiggerThan0Trigger(string.Empty);
                    break;
                case "HasSubscriptionTagTrigger":
                    key = new HasSubscriptionTagTrigger();
                    break;
                case "HasNotSubscriptionTagTrigger":
                    key = new HasNotSubscriptionTagTrigger();
                    break;
                case "StateIsValueTrigger":
                    key = new StateIsValueTrigger(string.Empty, string.Empty);
                    break;
                case "StateIsNotValueTrigger":
                    key = new StateIsNotValueTrigger(string.Empty, string.Empty);
                    break;
                case "StateIsNotValueEmpty":
                    key = new StateIsNotValueEmpty();
                    break;
                case "StateIsValueEmpty":
                    key = new StateIsValueEmpty();
                    break;
                case "PropertyUsernameIsTrigger":
                    key = new PropertyUsernameIsTrigger(new List<string>());
                    break;
                case "PropertyUsernameIsNotTrigger":
                    key = new PropertyUsernameIsNotTrigger(new List<string>());
                    break;
                case "MessageStartWithTrigger":
                    key = new MessageStartWithTrigger(string.Empty);
                    break;
                default:
                    throw new Exception("Trigger type not found.");
            }

            var keyJsonReader = triggerToken["Key"].CreateReader();
            var priority = triggerToken["Priority"].Value<int>();
            key.Priority = priority;
            serializer.Populate(keyJsonReader, key);
            return key;
        }

        public static ITrigger ReadComplexAndTrigger(ITrigger key, JToken triggerToken, JsonSerializer serializer)
        {
            foreach (var childTrigger in triggerToken.Children())
            {
                ((AndTrigger)key).Triggers.Add(DeserializeTrigger(serializer, childTrigger));
            }
            return key;
        }

        public static ITrigger ReadComplexOrTrigger(ITrigger key, JToken triggerToken, JsonSerializer serializer)
        {
            foreach (var childTrigger in triggerToken.Children())
            {
                ((OrTrigger)key).Triggers.Add(DeserializeTrigger(serializer, childTrigger));
            }
            return key;
        }

        public static void ReadActionList(JsonSerializer serializer, JToken jToken, List<FunnelAction> actions)
        {
            foreach (var actionToken in jToken.Children())
            {
                var action = default(FunnelAction);
                switch (actionToken["$type"].Value<string>())
                {
                    case "MessageAction":
                        action = new MessageAction(string.Empty);
                        break;
                    case "MessageButtonAction":
                        action = new MessageButtonAction(string.Empty, new List<List<string>>());
                        break;
                    case "MessageMenuButtonAction":
                        action = new MessageMenuButtonAction(string.Empty, new List<List<string>>());
                        break;
                    case "MessageInputAction":
                        action = new MessageInputAction(string.Empty);
                        break;
                    case "WidgetMessageAction":
                        action = new WidgetMessageAction(string.Empty);
                        break;
                    case "WidgetMessageForKeyAction":
                        action = new WidgetMessageForKeyAction(string.Empty);
                        break;
                    case "WidgetMessageButtonAction":
                        action = new WidgetMessageButtonAction(string.Empty, new List<List<Button>>());
                        break;
                    case "ClientCreateAction":
                        action = new ClientCreateAction();
                        break;
                    case "EmailUpdateAction":
                        action = new EmailUpdateAction();
                        break;
                    case "MessageShowSubscriptionsAction":
                        action = new MessageShowSubscriptionsAction();
                        break;
                    case "PaymentInfoCreateActionAsync":
                        action = new PaymentInfoCreateActionAsync();
                        break;
                    case "CgPaymentInfoCreateActionAsync":
                        action = new CgPaymentInfoCreateActionAsync();
                        break;
                    case "StateUpdateAction":
                        action = new StateUpdateAction(string.Empty, string.Empty);
                        break;
                    case "StateUpdateActionWithData":
                        action = new StateUpdateActionWithData(string.Empty);
                        break;
                    case "SubscriptionCreateAction":
                        action = new SubscriptionCreateAction();
                        break;
                    case "ChannelAddMyselfActionAsync":
                        action = new ChannelAddMyselfActionAsync();
                        break;
                    case "CommandChannelAddClientActionAsync":
                        action = new CommandChannelAddClientActionAsync();
                        break;
                    case "ClientRequesTalkActionAsync":
                        action = new ClientRequesTalkActionAsync();
                        break;
                    case "ChannelAddClientActionAsync":
                        action = new ChannelAddClientActionAsync();
                        break;
                    case "ImageActionAsync":
                        action = new ImageActionAsync();
                        break;
                    case "FindChatIdActionAsync":
                        action = new FindChatIdActionAsync();
                        break;
                    case "FindChatIdFromUsernameActionAsync":
                        action = new FindChatIdFromUsernameActionAsync();
                        break;
                    case "CallMeEmailAsync":
                        action = new CallMeEmailAsync();
                        break;
                    case "IvoHttpNotificationActionAsync":
                        action = new IvoHttpNotificationActionAsync();
                        break;
                    case "IvoHttpGetSubscriptionActionAsync":
                        action = new IvoHttpGetSubscriptionActionAsync();
                        break;
                    case "CommandIvoGetSubscriptionAsync":
                        action = new CommandIvoGetSubscriptionAsync();
                        break;
                    case "IvoSendAboutToExpireNotificationAsync":
                        action = new IvoSendAboutToExpireNotificationAsync();
                        break;
                    case "ChannelUnBanMyselfActionAsync":
                        action = new ChannelUnBanMyselfActionAsync();
                        break;
                    case "CommandChannelUnBanActionAsync":
                        action = new CommandChannelUnBanActionAsync();
                        break;
                    case "CommandMessageActionAsync":
                        action = new CommandMessageActionAsync(string.Empty);
                        break;
                    case "CommandClientMessageActionAsync":
                        action = new CommandClientMessageActionAsync(string.Empty);
                        break;
                    case "TgClientMessageActionAsync":
                        action = new TgClientMessageActionAsync(string.Empty);
                        break;
                    case "TgClientSendImageActionAsync":
                        action = new TgClientSendImageActionAsync(string.Empty, string.Empty);
                        break;
                    case "SetUsernameCommandAction":
                        action = new SetUsernameCommandAction();
                        break;
                    case "FindChatIdFromMessageTextActionAsync":
                        action = new FindChatIdFromMessageTextActionAsync();
                        break;
                    case "FindChatIdCommandByUsernameActionAsync":
                        action = new FindChatIdCommandByUsernameActionAsync();
                        break;
                    case "CleanUpChannelActionAsync":
                        action = new CleanUpChannelActionAsync();
                        break;
                    case "BiCreateLimitOrderAsync":
                        action = new BiCreateLimitOrderAsync();
                        break;
                    case "BiCreateOrderAsync":
                        action = new BiCreateOrderAsync();
                        break;
                    case "ExchangeUpdateAction":
                        action = new ExchangeUpdateAction();
                        break;
                    case "BiGetOpenOrdersAsync":
                        action = new BiGetOpenOrdersAsync();
                        break;
                    case "BiEnrichParametersAsync":
                        action = new BiEnrichParametersAsync();
                        break;
                    case "BiShowOpenOrdersAsync":
                        action = new BiShowOpenOrdersAsync();
                        break;
                    case "BiCancelOpenOrderAsync":
                        action = new BiCancelOpenOrderAsync();
                        break;
                    case "BiMessageResultAction":
                        action = new BiMessageResultAction();
                        break;
                    case "BiShowButtonsCancelOpenOrdersAsync":
                        action = new BiShowButtonsCancelOpenOrdersAsync();
                        break;
                    case "PaypalPaymentCreateActionAsync":
                        action = new PaypalPaymentCreateActionAsync();
                        break;
                    default:
                        throw new Exception("Action type not found.");
                }

                var keyJsonReader = actionToken["Value"].CreateReader();
                serializer.Populate(keyJsonReader, action);
                actions.Add(action);
            }
        }
    }
}
