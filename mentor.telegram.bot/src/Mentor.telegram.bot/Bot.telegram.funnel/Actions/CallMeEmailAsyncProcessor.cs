using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.api.services.Services.Email;

namespace Bot.telegram.funnel.Actions
{
    public class CallMeEmailAsyncProcessor : ActionProcessorAsync<CallMeEmailAsync>
    {
        private readonly IEmailService _emailService;
        private readonly ILanguageService _languageService;

        public CallMeEmailAsyncProcessor(IEmailService emailService, ILanguageService languageService)
        {
            _emailService = emailService;
            _languageService = languageService;
        }

        public override async Task<bool> ExecuteAsync(CallMeEmailAsync action, Dictionary<string, object> parameters)
        {
            Log.LogDeb("CallMeEmailAsyncProcessor executing ", action.CorrelationId);

            var phoneNumber = parameters.GetMessageText();

            var result = await _emailService.Send(new SendEmailEvent()
            {
                BodyHtml = "",
                EmailTo = action.Email,
                CorrelationId = action.CorrelationId,
                Subject = $"[{_languageService.Get(parameters.GetLanguage(), "CallMe")}] {phoneNumber}",
                PlainTextContent = _languageService.Get(parameters.GetLanguage(), "CallMeEmail", phoneNumber)
            });

            return result;
        }
    }
}