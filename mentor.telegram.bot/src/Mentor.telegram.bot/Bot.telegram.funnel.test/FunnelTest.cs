using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Bot.telegram.funnel.test
{
    public class FunnelTest
    {
        private const string StartMessage = "\\start";
        private const string WelcomeMessage = "Welcome to CryptoTraders";
        private const string ButtonBuyCourse = "Buy Course";
        private const string ButtonGoBack = "Go Back";
        private const string CourseSelectionMessage = "Which course would you like to buy:";
        private const string ButtonBegginer = "Begginer Trading";
        private const string ButtonIntermidiate = "Intermidiate Trading";
        private const string ButtonAdvance = "Advance Trading";
        private const string ButtonPro = "Proffesional Trading";
        [Fact]
        public async Task WhenProccessCallbackOfAEmptyFunnelItReturnTrue()
        {
            IFunnel testFunnel = GetEmptyFunnel();
            var result = await testFunnel.ProcessCallbacks(new Data(), null, null, Guid.NewGuid());
            Assert.True(result);
        }

        [Fact]
        public async Task WhenProccessMessagesOfAEmptyFunnelItReturnTrue()
        {
            IFunnel testFunnel = GetEmptyFunnel();
            var result = await testFunnel.ProcessCallbacks(new Data(), null, null, Guid.NewGuid());
            Assert.True(result);
        }

        [Fact]
        public async Task WhenProccessMessagesFunnelWithAMessageUiIsCalledAndReturnTrue()
        {
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            serviceCollection.AddTransient<ActionProcessor<MessageAction>, MessageActionProcessor>();
            serviceCollection.AddTransient<ActionProcessor<MessageButtonAction>, MessageButtonActionProcessor>();

            Mock<IUserInterfaceService> userMock = new Mock<IUserInterfaceService>();
            serviceCollection.AddTransient<IUserInterfaceService>(x => userMock.Object);

            IFunnel testFunnel = GetWelcomeFunnel();
            var result = await testFunnel.ProcessMessages(new Data()
            {
                Message = StartMessage
            }, null, serviceCollection.BuildServiceProvider(), Guid.NewGuid());

            Assert.True(result);
            userMock.Verify(x => x.SendMessage(WelcomeMessage, null));
        }

        [Fact]
        public async Task WhenProccessCallbacksInWelcomeFunnelUiIsNotCalled()
        {
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            serviceCollection.AddTransient<ActionProcessor<MessageAction>, MessageActionProcessor>();
            serviceCollection.AddTransient<ActionProcessor<MessageButtonAction>, MessageButtonActionProcessor>();

            Mock<IUserInterfaceService> userMock = new Mock<IUserInterfaceService>();
            serviceCollection.AddTransient<IUserInterfaceService>(x => userMock.Object);

            IFunnel testFunnel = GetWelcomeFunnel();

            var result = await testFunnel.ProcessCallbacks(new Data()
            {
                Message = StartMessage
            }, null, serviceCollection.BuildServiceProvider(), Guid.NewGuid());

            Assert.True(result);
            userMock.Verify(x => x.SendMessage(WelcomeMessage, null), Times.Never());
        }

        [Fact]
        public async Task SubscriptionFunnelCallTheRightMethods()
        {
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            serviceCollection.AddTransient<ActionProcessor<MessageAction>, MessageActionProcessor>();
            serviceCollection.AddTransient<ActionProcessor<MessageButtonAction>, MessageButtonActionProcessor>();

            Mock<IUserInterfaceService> userMock = new Mock<IUserInterfaceService>();
            serviceCollection.AddTransient<IUserInterfaceService>(x => userMock.Object);

            IFunnel testFunnel = GetSubscriptionFunnel();

            var result = await testFunnel.ProcessMessages(new Data()
            {
                Message = StartMessage
            }, null, serviceCollection.BuildServiceProvider(), Guid.NewGuid());
            Assert.True(result);
            userMock.Verify(x => x.SendMessageWithButtons(WelcomeMessage, It.IsAny<List<List<string>>>(), null), Times.Once());

            result = await testFunnel.ProcessCallbacks(new Data()
            {
                Message = ButtonBuyCourse
            }, null, serviceCollection.BuildServiceProvider(), Guid.NewGuid());
            Assert.True(result);
            userMock.Verify(x => x.SendMessageWithButtons(CourseSelectionMessage, It.IsAny<List<List<string>>>(), null), Times.Once());

            result = await testFunnel.ProcessCallbacks(new Data()
            {
                Message = ButtonAdvance
            }, null, serviceCollection.BuildServiceProvider(), Guid.NewGuid());
            Assert.True(result);
            userMock.Verify(x => x.SendMessage($"Welcome to {ButtonAdvance}", null), Times.Once());

        }

        private IFunnel GetEmptyFunnel()
        {
            var result = new Funnel();
            return result;
        }

        private IFunnel GetWelcomeFunnel()
        {
            var result = new Funnel();
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageAction(WelcomeMessage)
            });
            return result;
        }

        private IFunnel GetSubscriptionFunnel()
        {
            var result = new Funnel();
            List<List<string>> botOptions = new List<List<string>>()
            {
                new List<string>(){ ButtonBuyCourse, ButtonGoBack},
            };
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageButtonAction(WelcomeMessage,botOptions)
            });

            List<List<string>> courseOptions = new List<List<string>>()
            {
                new List<string>(){ButtonBegginer, ButtonIntermidiate},
                new List<string>(){ButtonAdvance, ButtonPro}
            };

            result.CallbackActions.Add(new MessageTrigger(ButtonBuyCourse), new List<FunnelAction>()
            {
                new MessageButtonAction(CourseSelectionMessage, courseOptions)
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonBegginer), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonBegginer}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonIntermidiate), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonIntermidiate}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonAdvance), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonAdvance}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonPro), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonPro}")
            }); 
            return result;
        }
    }
}
