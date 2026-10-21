using System.Collections.Generic;

namespace Mentor.bot.boundedContext.Services
{
    public interface ILanguageService
    {
        string Get(string language, string key);
        string Get(string language, string key, params object[] parameters);
    }

    public class LanguageService : ILanguageService
    {
        private readonly Dictionary<string, string> _words;
        public string PleaseTypePhoneNumberMessage
        {
            get { return "Please introduce your phone number and we will call you shortly."; }
        }
        public LanguageService()
        {
            _words = new Dictionary<string, string>()
            {
                { "ES-CallMeEmail","Has recibido una petición de llamada desde la ventana del Bot para el numero {0}"},
                { "EN-CallMeEmail","You have a callback request on the following number {0}"},
                { "ES-CallMe","Llámame'"},
                { "EN-CallMe","Call me"},
                { "EN-PleaseTypePhoneNumberMessage","Please introduce your phone number and we will call you shortly."},
                { "ES-PleaseTypePhoneNumberMessage","Escribe tu número de teléfono y contactaremos con la mayor brevedad."},
            };
        }
        public string Get(string language, string key)
        {
            string dictionaryKey = $"{language.ToUpper()}-{key}";
            return _words[dictionaryKey];
        }

        public string Get(string language, string key, params object[] parameters)
        {
            string dictionaryKey = $"{language.ToUpper()}-{key}";
            return string.Format(_words[dictionaryKey], parameters);
        }
    }
}
