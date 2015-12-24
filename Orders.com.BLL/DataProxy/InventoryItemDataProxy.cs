using Orders.com.BLL.Domain;
using System.Threading.Tasks;

namespace Orders.com.BLL.DataProxy
{
    public interface IInventoryItemDataProxy : IOrdersDotComDataProxy<InventoryItem>
    {
        InventoryItem GetByProduct(long productID);
        Task<InventoryItem> GetByProductAsync(long productID);
    }
}
