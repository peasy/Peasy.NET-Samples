using Orders.com.BLL.Domain;
using Orders.com.BLL.DataProxy;
using Peasy;

namespace Orders.com.BLL.Services
{
    public class InventoryItemService : OrdersDotComServiceBase<InventoryItem>, IInventoryItemService
    {
        public InventoryItemService(IInventoryItemDataProxy dataProxy) : base(dataProxy)
        {
        }

        public ICommand<InventoryItem> GetByProductCommand(long productID)
        {
            var proxy = DataProxy as IInventoryItemDataProxy;
            return new ServiceCommand<InventoryItem>
            (
                executeMethod: () => proxy.GetByProduct(productID),
                executeAsyncMethod: () => proxy.GetByProductAsync(productID)
            );
        }
    }
}
