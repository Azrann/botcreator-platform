using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.telegram.funnel.Actions
{
    public class BiShowButtonsCancelOpenOrdersProcessor : ActionProcessorAsync<BiShowButtonsCancelOpenOrdersAsync>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public BiShowButtonsCancelOpenOrdersProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override async Task<bool> ExecuteAsync(BiShowButtonsCancelOpenOrdersAsync action,
            Dictionary<string, object> parameters)
        {
            var openTrades = parameters.GetBiOpenTrades();

            var buttonList = new List<List<string>>();
            foreach (var order in openTrades)
            {
                string orderType = BiFormatter.GetOrderType(order.Type);
                buttonList.Add(new List<string>()
                {
                    $"🏷{order.OrderId} {order.Symbol} {orderType} {BiFormatter.GetSide(order.Side)}"
                });
            }
            buttonList.Add(new List<string>()
            {
                $"Main Menu 🔙"
            });

            await _userInterfaceService.RemovePreviousMessage(parameters);
            await _userInterfaceService.SendMessageWithButtons("Select which order do you want to cancel", buttonList
            , parameters);

            return true;
        }
    }

    public class BiShowButtonsCancelOpenOrdersAsync : FunnelAction
    {
    }
}
