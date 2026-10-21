using System;
using BotCreator.Core.Common.Models;
using BotCreator.Service.Bot;
using Core.Common.NoSqlDataAccess.DataAccess;
using Newtonsoft.Json;
using Xunit;

namespace BotCreator.Service.Test
{
    public class WidgetConfigStoreServiceTest
    {
        private const string Connection = "REPLACE_WITH_YOUR_VALUE";
        private const string BotId = "test123abc";

        [Fact]
        public void ICanInitializeTheStorageService()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            service.Initialize();
        }

        [Fact]
        public void WhenConfigDoesntExistsWeReturnNull()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var result = service.GetWidgetConfigById(Guid.NewGuid().ToString());

            Assert.Null(result);
        }

        [Fact]
        public void WhenICreateAConfigICanRetrieveIt()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = BotId,
                PrimaryColor = "rgb(0, 140, 217)",
                SecondaryColor = "rgb(106, 96, 164)",
                Language = "es",
                BubbleMessage = "Hey que pasa 👍",
                Description = "SuperDescripcion Pincha abajo 👇",
                ChatNow = "Hablar ahora"
            };

            service.InsertOrUpdate(BotId, widgetConfigData);

            var result = service.GetWidgetHtmlConfigById(BotId,true);
            Assert.Equal(widgetConfigData.ChatNow, result.ButtonsText.chat_now_btn);
            Assert.Equal(widgetConfigData.BubbleMessage, result.first_message_text);
        }

        [Fact]
        public void ICanSerializeWidgetConfigWithUnicode()
        {
            var themeColor = "rgb(0, 140, 217)";

            var config = WidgetConfig.NewWidgetEs("5ae3941c-51d2-4489-a7ce-c3339ee221ad", themeColor);

            config.ButtonsText.chat_now_btn = "Hablar ahora! ";
            config.first_message_text = "Una semana de Yoga por 5€?";
            config.description = "Ven a conocernos 👍, estamos en Avenida de Valencia 1, Denia. Alicante. Para saber mas pulsa en el botón de abajo 👇";

            var result = JsonConvert.SerializeObject(config);

        }

        [Fact]
        public void ICanCreateACustomConfigInCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = "5ae3941c-51d2-4489-a7ce-c3339ee221ad",
                PrimaryColor = "#4fe0c0",
                SecondaryColor = "rgb(106, 96, 164)",
                Language = "es",
                BubbleMessage = "Una semana de Yoga por 5€?",
                Description = "Ven a conocernos 👍, estamos en Avenida de Valencia 1, Denia. Alicante. Para saber mas pulsa en el botón de abajo 👇",
                ChatNow = "Hablamos?",
                BotName = "Hola, soy Naylín.",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/naylinavatar.jpg",
                Active = true
            };

            service.InsertOrUpdate("5ae3941c-51d2-4489-a7ce-c3339ee221ad", widgetConfigData);
        }

        [Fact]
        public void ICanCreateACustomConfigForNaylin2InCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = "b3d00cb9-790f-49c1-bc7d-0f92b6595f56",
                PrimaryColor = "rgb(0, 140, 217)",
                SecondaryColor = "rgb(106, 96, 164)",
                Language = "es",
                BubbleMessage = "👍 Programa de 8 Semanas de Mindfullness",
                Description = "Este programa está diseñado no solo para aportar de las técnicas de Mindfulness más comprobadas por su eficacia, sino para acompañar tu proceso personal. Una forma de conocer cuáles son los condicionamientos que te afectan en tu vida cotidiana y cómo dar los pasos para la transformación.</br> Para saber mas pulsa en el botón de abajo 👇",
                ChatNow = "Hablamos?",
                BotName = "Hola, soy Naylín.",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/naylinavatar.jpg",
                Active = true
            };

            service.InsertOrUpdate("b3d00cb9-790f-49c1-bc7d-0f92b6595f56", widgetConfigData);
        }


        [Fact]
        public void ICanCreateACustomConfigForEscuelaHumanistaInCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var botId = "aff23055-e571-4bb4-ad7c-478b73ff87dc";
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = botId,
                PrimaryColor = "#3f7ccd",
                SecondaryColor = "rgb(106, 96, 164)",
                Language = "es",
                BubbleMessage = "🙆 Conoce nuestro retiro de verano",
                Description = @"En Escuela humanista realizamos retiros de verano y talleres sobre habilidades sociales, sexualidad, autoestima y más</br>
                               Pulsa en el botón de abajo 👇",
                ChatNow = "Hablamos?",
                BotName = "Sergio García de Escuela Humanista",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/EscuelaHumanistaLogo2.png",
                Active = true
            };

            service.InsertOrUpdate(botId, widgetConfigData);
        }

        [Fact]
        public void ICanCreateACustomConfigForMetodErosInCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var botId = "0774041a-54d0-4943-b54d-5ce941b8fb29";
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = botId,
                PrimaryColor = "#c9995c",
                SecondaryColor = "rgb(106, 96, 164)",
                Language = "es",
                BubbleMessage = "Descubre el método más eficaz para encontrar pareja!",
                Description = @"Disponemos de un método único y multidisciplinar, donde ensamblamos diferentes disciplinas, sistemas y procedimientos para crear el MÉTODO EROS.. </br>
                               Para saber mas pulsa en el botón de abajo 👇",
                ChatNow = "Hablamos?",
                BotName = "Bot de Metodo Eros",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/nuriapalomo.jpg",
                Active = true
            };

            service.InsertOrUpdate(botId, widgetConfigData);
        }

        [Fact]
        public void ICanCreateACustomConfigForAfricabosInCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var botId = "f08d4da5-27e6-4c8b-bb38-f9e81631b7fd";
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = botId,
                PrimaryColor = "#7d005c",
                SecondaryColor = "rgb(106, 96, 164)",
                HeaderTextColor = "rgb(230, 230, 230)",
                Language = "es",
                BubbleMessage = "🙆 Tenemos un nuevo master!",
                Description = @"AfricaBos te presenta el nuevo master en relaciones personales, la mejor formación hasta la fecha. </br>
                               Para saber mas pulsa en el botón de abajo 👇",
                ChatNow = "Hablamos?",
                BotName = "El bot de AfricaBos",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/REPLACE_WITH_YOUR_VALUE.jpg",
                Active = true
            };

            service.InsertOrUpdate(botId, widgetConfigData);
        }

        [Fact]
        public void ICanCreateACustomConfigYoeducoInCode()
        {
            var service = new WidgetConfigStorageService(new DapperNoSql<WidgetConfigData>(Connection));
            var botId = "25573ee3-f87e-4d91-92a3-ee9d708b6a4a";
            var widgetConfigData = new WidgetConfigData()
            {
                BotId = botId,
                PrimaryColor = "#ed3f27",
                SecondaryColor = "rgb(106, 96, 164)",
                HeaderTextColor = "rgb(230, 230, 230)",
                Language = "es",
                BubbleMessage = "🚀 bot Yoeduco, ¿tienes dudas?",
                Description = @"Queremos formar a tu peque en materias del currículum escolar desde un punto de vista diferente a lo cotidiano, práctico, creativo y divertido. </br>
                               Para saber mas pulsa en el botón de abajo 👇",
                ChatNow = "Hablar ahora!",
                BotName = "El bot de YoEduco",
                BotAvatarUrl = "https://botcreator.azureedge.net/images/yoeduco.PNG",
                RemovePrevious = true,
                Active = true,
                WidgetUrl = "https://botcreator.azureedge.net/javascript/helpWidget-0010h.js"
            };

            service.InsertOrUpdate(botId, widgetConfigData);
        }
        
    }
}
