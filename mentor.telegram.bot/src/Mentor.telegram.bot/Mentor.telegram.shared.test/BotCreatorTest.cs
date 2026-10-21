using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Bot.telegram.funnel;
using Mentor.telegram.shared.BotTemplates;
using Mentor.telegram.shared.Services;
using Xunit;

namespace Mentor.telegram.shared.test
{
    public class BotCreatorTest
    {
        private const string ConnectionString = @"REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void ICanCreateABasicBot()
        {
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);
            
            // Create the bot.
            BasicChatBot basicChatBot = new BasicChatBot()
            {
                ConnectingToAgentMessage = "Connecting to Agent..."
            };

            string testBotId = "testBotId123";

            botDefinitionStorageService.InsertOrUpdate(testBotId, basicChatBot);

            // Deploy the bot.
            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanRedeployAbot()
        {
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            string testBotId = "5bc2d86d-dbb2-48bd-b95b-4e276561103c";

            // Deploy the bot.
            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotWithoutChat()
        {
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = false,
                CallMeChecked = true,
                CallMeEmail = "user@example.invalid",
                CallMeButtonText = "llamame",
                PreMenuText = "Options:",
                MenuBot = new Dictionary<string, string>()
                {
                    {"Contact info:","You can find us at /n 14 House CT43GF Phone number:07544588556"},
                    {"Delivery", "Deliveries will take between 3 and 5 days if you don't get it contact us at user@example.invalid" },
                    {"Discount codes", "At the moment we are offering 10% with code 'Smart10' we change discounts every week keep in touch for the best deals." }
                }
            };

            string testBotId = "testBotId456a";

            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotWithChat()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = true,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                TalkToAgent = "Speak to Agent",
                PreMenuText = "Options:",
                WhatsappChecked = true,
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                MenuBot = new Dictionary<string, string>()
                {
                    {"Contact us","You can find us at /n 14 House CT43GF Phone number:07544588556"},
                    {"Delivery time", "Deliveries will take between 3 and 5 days if you don't get it contact us at user@example.invalid" },
                    {"Codes", "At the moment we are offering 10% with code 'Smart10' we change discounts every week keep in touch for the best deals." }
                }
            };

            string testBotId = "testBotId456b";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanDeployABot()
        {
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));
            botDefinitionStorageService.Initialize();
            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            var botId = "b3d00cb9-790f-49c1-bc7d-0f92b6595f56";

            var result = creator.DeployBot(botId);
            Assert.True(result);

        }


        [Fact]
        public void ICanCreateAWidgetSupportBoForOfferWithChat()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = true,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                MenuBot = new Dictionary<string, string>()
                {
                    {"¿Cómo sé si es para mí ✋?",@"Si alguna de estas inquietudes surgen en ti, hablemos:
                        </br>👉 Quieres conocerte mejor, saber cómo funcionan tu mente y tus emociones
                        </br>👉 Te gustaría saber cuáles son las causas del sufrimiento y experimentar otra forma de ver la vida.
                        </br>👉 Sientes un vacío que no sabes cómo gestionar"},
                    {"¿Cómo hacemos para hablar?","Llámame al 635 99 36 58, si no te salgo yo misma, pregunta por Naylín" }
                }
            };

            string testBotId = "b3d00cb9-790f-49c1-bc7d-0f92b6595f56";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotFor5EuroOfferWithChat()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = true,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                MenuBot = new Dictionary<string, string>()
                {
                    {"¿La oferta es verdad? 🙋",@"Sí 😊, todas las clases durante una semana por solo 5 euros"},
                    {"¿Y cuando termine la semana?", "✅ Pues podrás disfrutas de dos clases a la semana durante tres meses por 100€ con el Bono de Bienvenida." },
                    {"¿Qué pasa si no soy flexible, nunca he practicado yoga o creo que no podré hacer los ejercicios? ✏", "No tienes que cumplir con ningún ejercicio para comenzar. Con la práctica regular tu cuerpo, mente y emociones irán transformándose, a su ritmo." },
                    {"¿Qué hago si quiero ir a probar?", "✅ Mándanos un Whatsapp o llámanos al 635 99 36 58, diciéndonos cuál de las clases será la primera que irás" },
                }
            };

            string testBotId = "5ae3941c-51d2-4489-a7ce-c3339ee221ad";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotForEscuelaHumanista()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = true,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                MenuBot = new Dictionary<string, string>()
                {
                    {"Formarme 🙋",@"Impartimos:</br>🔸 Formación en Terapia Gestalt (Link a página correspondiente)
                                    </br>🔸 Formación en Habilidades Sociales y Gestión Emocional
                                    </br>🔸 Formación en Sexualidad Consciente
                                    </br>🔸 Formación en Arteterapia "},
                    {"Retiros Intensivos", @"Tenemos:
                                </br>🔸 Retiro Verano Sexualidad
                                </br>🔸 Retiro Verano Despierta tu Fuerza
                                </br>🔸 Retiro Semana Santa Despierta tu Fuerza
                                </br>🔸 Retiro Navidad Despierta tu Fuerza" },
                    {"Autoestima", @"Recomendamos:</br>🔸 Formación en Gestalt
                                           </br>🔸 Retiro Despierta tu Fuerza
                                            </br>🔸 Talleres de Fin de semana" },
                    {"Sobre Sexualidad", @"Recomendamos:</br>🔸 Formación en Sexualidad Consciente
                                        </br>🔸 Retiro de Sexualidad
                                        </br>🔸 Talleres de Sexualidad" }
                },
                CallMeChecked = true,
                CallMeEmail = "user@example.invalid",
                CallMeButtonText = "Llámame",
            };

            string testBotId = "aff23055-e571-4bb4-ad7c-478b73ff87dc";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotForMetodoEros()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = true,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                MenuBot = new Dictionary<string, string>()
                {
                    {"Es solo para encontrar pareja 💖?","No, estamos entusiasmados con la idea de que amplíes tu círculo de amistades tambien."},
                    {"En que ciudades puedo apuntarme?", @"Trabajamos tanto en Madrid como en Barcelona" },
                    {"¿Como me apunto? ✏", "Pincha en <a href='http://www.metodoeros.com/contacto/'>este enlace</a> y envíanos tus datos. O háblanos a traves de WhatsApp." }
                },
                CallMeChecked = true,
                CallMeEmail = "user@example.invalid",
                CallMeButtonText = "Llámame",
            };

            string testBotId = "0774041a-54d0-4943-b54d-5ce941b8fb29";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotForAfricaBos()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = true,
                FbUserName = "REPLACE_WITH_YOUR_VALUE",
                WhatsappChecked = false,
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                MenuBot = new Dictionary<string, string>()
                {
                    {"¿Donde y cuando se realizan? 🙋","El master se lleva a cabo en Madrid y Barcelona. Empezamos el dia ... 📆"},
                    {"¿Que puedo conseguir con este master?", @"Pues podrás 
                                </br>🔸 Mejorar tus habilidades sociales.
                                </br>🔸 Aprender a gestionar tus emociones.
                                </br>🔸 Ampliar tu circulo de amigos." },
                    {"¿Como me apunto? ✏", "Pincha en <a href='ponelenlaceaqui'>este enlace</a> y envíanos tus datos." }
                },
                CallMeChecked = true,
                CallMeEmail = "africa@REPLACE_WITH_YOUR_VALUE.com",
                CallMeButtonText = "Llámame",
            };

            string testBotId = "f08d4da5-27e6-4c8b-bb38-f9e81631b7fd";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }

        [Fact]
        public void ICanCreateAWidgetSupportBotForYoeduco()
        {
            // This is setting up the services I need, with Dependency injection you will not need that.
            IFunnelStorageService funnelStorageService = new FunnelStorageService(new DapperNoSql<Funnel>(ConnectionString));
            IBotDefinitionStorageService botDefinitionStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            IBotStorageService botStorageService = new BotStorageService(new DapperNoSql<Bot.telegram.funnel.Bot>(ConnectionString));

            BotCreator creator = new BotCreator(funnelStorageService, botStorageService, botDefinitionStorageService);

            // Creating the bot, The menu bot is a pair , the KEY is button Text the VALUE is the text response
            // It will be more clear what part is which once you deploy a bot.
            WidgetSupportBot widgetSupportBot = new WidgetSupportBot()
            {
                ChatOnWeb = false,
                FbChecked = true,
                FbUserName = "REPLACE_WITH_YOUR_VALUE",
                WhatsappChecked = true,
                WhatsappNumber = "REPLACE_WITH_YOUR_VALUE",
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                MenuBot = new Dictionary<string, string>()
                {
                    {"¿Cuándo empieza la escuela de verano? 🙋","La Escuela de verano abrirá sus puertas desde el 1 de Julio al 30 de Agosto."},
                    {"¿Que precio tiene?", @"Puedes pagar por mes completo o por semanas.
                                </br>🔸 120€ mes completo.
                                </br>🔸 100€ 3 semanas.
                                </br>🔸 65€ 2 semanas.
                                </br>🔸 35€ 1 semana.
                                </br> Para mas info estamos en Whatsaap Facebook o puedes visitar la <a href='https://escuelaveranomarbella.com/precios/'>pagina de precios</a>." },
                    {"¿Como me apunto? ✏", "Pincha en la <a href='https://escuelaveranomarbella.com/inscripcion/'>pagina de inscripción</a> y sigue los pasos." }
                },
                CallMeChecked = true,
                CallMeEmail = "user@example.invalid",
                CallMeButtonText = "Llámame",
            };

            string testBotId = "25573ee3-f87e-4d91-92a3-ee9d708b6a4a";

            // First we save the configuration of the bot, then deploy it.
            botDefinitionStorageService.InsertOrUpdate(testBotId, widgetSupportBot);

            var result = creator.DeployBot(testBotId);

            Assert.True(result);
        }
    }
}
