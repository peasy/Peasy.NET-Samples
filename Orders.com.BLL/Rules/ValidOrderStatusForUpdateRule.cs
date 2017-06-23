using System.Linq;
using System.Threading.Tasks;
using Orders.com.BLL.Services;
using Orders.com.BLL.Extensions;
using Peasy;

namespace Orders.com.BLL.Rules
{
    public class ValidOrderStatusForUpdateRule : RuleBase
    {
        private long _orderID;
        private IOrderItemService _orderItemDataProxy;

        public ValidOrderStatusForUpdateRule(long orderID, IOrderItemService orderItemDataProxy)
        {
            _orderID = orderID;
            _orderItemDataProxy = orderItemDataProxy;
        }

        protected override void OnValidate()
        {
            var items = _orderItemDataProxy.GetByOrderCommand(_orderID).Execute().Value;
            if (items.Any(i => i.OrderStatus() is ShippedState))
            {
                Invalidate("This order cannot change because it has items that have been shipped");
            }
        }

        protected override async Task OnValidateAsync()
        {
            var items = await _orderItemDataProxy.GetByOrderCommand(_orderID).ExecuteAsync();
            if (items.Value.Any(i => i.OrderStatus() is ShippedState))
            {
                Invalidate("This order cannot change because it has items that have been shipped");
            }
        }
    }
}
