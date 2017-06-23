using Orders.com.BLL.Domain;
using Peasy;

namespace Orders.com.BLL.Services
{
    public interface IInventoryItemService : IService<InventoryItem, long>
    {
        ICommand<InventoryItem> GetByProductCommand(long productID);
    }
}