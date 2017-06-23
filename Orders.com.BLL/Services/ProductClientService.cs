using Peasy;
using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.BLL.Services
{
    public class ProductClientService : ProductService, IProductService
    {
        public ProductClientService(IProductDataProxy dataProxy, IOrderDataProxy orderDataProxy, IInventoryItemService inventoryService, ITransactionContext transactionContext) : base(dataProxy, orderDataProxy, inventoryService, transactionContext)
        {
        }

        public override ICommand<Product> InsertCommand(Product entity)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new ServiceCommand<Product>
            (
                executeMethod: () => dataProxy.Insert(entity), 
                executeAsyncMethod: () => dataProxy.InsertAsync(entity)
            );
        }

        public override ICommand DeleteCommand(long id)
        {
            var dataProxy = DataProxy as IProductDataProxy;
            return new ServiceCommand
            (
                executeMethod: () => dataProxy.Delete(id), 
                executeAsyncMethod: () => dataProxy.DeleteAsync(id)
            );
        }
    }
}
