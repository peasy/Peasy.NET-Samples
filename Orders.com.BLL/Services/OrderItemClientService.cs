using Peasy;
using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.BLL.Services
{
    public class OrderItemClientService : OrderItemService
    {
        private ITransactionContext _transactionContext;
        private IInventoryItemDataProxy _inventoryDataProxy;

        public OrderItemClientService(IOrderItemDataProxy dataProxy,
                                      IProductDataProxy productDataProxy,
                                      IInventoryItemDataProxy inventoryDataProxy,
                                      ITransactionContext transactionContext) : base(dataProxy, productDataProxy, inventoryDataProxy, transactionContext)
        {
            _inventoryDataProxy = inventoryDataProxy;
            _transactionContext = transactionContext;
        }

        public override ICommand<OrderItem> ShipCommand(long orderItemID)
        {
            var proxy = DataProxy as IOrderItemDataProxy;
            return new ServiceCommand<OrderItem>
            (
                executeMethod: () => { return proxy.Ship(new OrderItem { OrderItemID = orderItemID }); },
                executeAsyncMethod: () => { return proxy.ShipAsync(new OrderItem { OrderItemID = orderItemID }); }
            );
        }
    }
}
