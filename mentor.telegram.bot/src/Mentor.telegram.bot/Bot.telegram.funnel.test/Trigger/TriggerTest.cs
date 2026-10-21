using System.Collections.Generic;
using Bot.telegram.funnel.Triggers;
using Xunit;

namespace Bot.telegram.funnel.test.Trigger
{
    public class TriggerTest
    {
        [Fact]
        public void WhenCheckMessageTriggerReturnTrue()
        {
            string testMessage = "\\start";
            ITrigger trigger = new MessageTrigger(testMessage);
            Data data = new Data()
            {
                Message = testMessage,
                Properties = null,
                Tags = new Dictionary<string, int>()
            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckMessageTriggerReturnFalse()
        {
            string testMessage = "\\start";
            ITrigger trigger = new MessageTrigger(testMessage);
            Data data = new Data()
            {
                Message = "\\start2",
                Properties = null,
                Tags = new Dictionary<string, int>()
            };

            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckTagTriggerReturnTrue()
        {
            string testMessage = "\\start";
            ITrigger trigger = new TagsAddedTrigger(new List<string>(){testMessage});
            var tags = new Dictionary<string, int>();
            tags.Add(testMessage,0);

            Data data = new Data()
            {
                Message = "\\start2",
                Properties = null,
                Tags = tags

            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckTagTriggerReturnFalse()
        {
            string testMessage = "\\start";
            ITrigger trigger = new TagsAddedTrigger(new List<string>() { testMessage });
            var tags = new Dictionary<string, int>();
            tags.Add("\\start2", 0);

            Data data = new Data()
            {
                Message = "\\start",
                Properties = null,
                Tags = tags

            };

            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertiesTriggerReturnTrue()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage,testValue);
            ITrigger trigger = new PropertiesTrigger(properties);
            
            Data data = new Data()
            {
                Message = "\\start2",
                Properties = properties,
            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertiesTriggerReturnFalse()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var properties2 = new Dictionary<string, object>();
            properties2.Add(testMessage, "test");
            ITrigger trigger = new PropertiesTrigger(properties);

            Data data = new Data()
            {
                Message = "\\start2",
                Properties = properties2,
            };

            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertiesTriggerReturnFalse2()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var properties2 = new Dictionary<string, object>();
            properties2.Add("MessageId1", testValue);
            ITrigger trigger = new PropertiesTrigger(properties);

            Data data = new Data()
            {
                Message = "\\start2",
                Properties = properties2,
            };

            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertiesStartWithTriggerReturnFalse()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var properties2 = new Dictionary<string, object>();
            properties2.Add(testMessage, "test A98ADJ1");
            ITrigger trigger = new PropertiesStartWithTrigger(properties);

            Data data = new Data()
            {
                Message = "\\start2",
                Properties = properties2,
            };

            Assert.False(trigger.Check(data));
        }


        [Fact]
        public void WhenCheckPropertiesStartWithTriggerReturnTrue()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var properties2 = new Dictionary<string, object>();
            properties2.Add(testMessage, "A98ADJ1 test");
            ITrigger trigger = new PropertiesStartWithTrigger(properties);

            Data data = new Data()
            {
                Message = "\\start2",
                Properties = properties2,
            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckAndTriggerReturnFalse()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var trigger = new AndTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("\\start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Message = "\\start"
            };
            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckAndTriggerReturnTrue()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var trigger = new AndTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("\\start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Message = "\\start",
                Properties = properties
            };
            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckOrTriggerReturnTrue()
        {
            string testMessage = "MessageId";
            string testValue = "A98ADJ1";
            var properties = new Dictionary<string, object>();
            properties.Add(testMessage, testValue);

            var trigger = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("//start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Properties = properties
            };
            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertyTriggerReturnTrue()
        {
            string testMessage = "MessageId";
            int testValue = 100;
            long testValue2 = 100;

            var properties = new Dictionary<string, object> {{testMessage, testValue}};
            var properties2 = new Dictionary<string, object> {{testMessage, testValue2}};

            var trigger = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("//start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Properties = properties2
            };
            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertyTriggerDecimalReturnTrue()
        {
            string testMessage = "MessageId";
            decimal testValue = 100;
            long testValue2 = 100;

            var properties = new Dictionary<string, object> { { testMessage, testValue } };
            var properties2 = new Dictionary<string, object> { { testMessage, testValue2 } };

            var trigger = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("//start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Properties = properties2
            };
            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckPropertyTriggerDecimalDoubleReturnTrue1()
        {
            string testMessage = "MessageId";
            decimal testValue = 100;
            double testValue2 = 100;

            var properties = new Dictionary<string, object> { { testMessage, testValue } };
            var properties2 = new Dictionary<string, object> { { testMessage, testValue2 } };

            var trigger = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("//start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Properties = properties2
            };
            Assert.True(trigger.Check(data));
        }


        [Fact]
        public void WhenCheckPropertyTriggerDecimalDoubleReturnTrue2()
        {
            string testMessage = "MessageId";
            decimal testValue = 0.000001m;
            double testValue2 = 0.000001;

            var properties = new Dictionary<string, object> { { testMessage, testValue } };
            var properties2 = new Dictionary<string, object> { { testMessage, testValue2 } };

            var trigger = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("//start"),
                    new PropertiesTrigger(properties)
                }
            };

            Data data = new Data()
            {
                Properties = properties2
            };
            Assert.True(trigger.Check(data));
        }


        [Fact]
        public void WhenCheckStateIsValueTriggerReturnTrue()
        {
            string testState = "state1";
            string testValue = "value1";
          
            
            var trigger = new StateIsValueTrigger(testState,testValue)
            {
               Priority = 100,
            };

            Data data = new Data()
            {
                State = new Dictionary<string, string>()
                {
                    { testState, testValue}
                }
            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckStateIsValueTriggerReturnFalse()
        {
            string testState = "state1";
            string testValue = "value2";
            string testValue2 = "value1";

            var trigger = new StateIsValueTrigger(testState, testValue)
            {
                Priority = 100,
            };

            Data data = new Data()
            {
                State = new Dictionary<string, string>()
                {
                    { testState, testValue2}
                }
            };

            Assert.False(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckStateIsNotValueTriggerReturnTrue()
        {
            string testState = "state1";
            string testValue = "value2";
            string testValue2 = "value1";

            var trigger = new StateIsNotValueTrigger(testState, testValue)
            {
                Priority = 100,
            };

            Data data = new Data()
            {
                State = new Dictionary<string, string>()
                {
                    { testState, testValue2}
                }
            };

            Assert.True(trigger.Check(data));
        }

        [Fact]
        public void WhenCheckStateIsNotValueTriggerReturnFalse()
        {
            string testState = "state1";
            string testValue = "value1";

            var trigger = new StateIsNotValueTrigger(testState, testValue)
            {
                Priority = 100,
            };

            Data data = new Data()
            {
                State = new Dictionary<string, string>()
                {
                    { testState, testValue}
                }
            };

            Assert.False(trigger.Check(data));
        }
    }
}
