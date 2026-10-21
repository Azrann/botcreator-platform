using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.telegram.funnel.Actions
{
    public class ImageActionProcessorAsync : ActionProcessorAsync<ImageActionAsync>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public ImageActionProcessorAsync(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override async Task<bool> ExecuteAsync(ImageActionAsync action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                await _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            await _userInterfaceService.SendImage(action.FileName, parameters, action.RemoveKeyboard);
            return true;
        }
    }
}