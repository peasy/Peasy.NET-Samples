using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class InventoryItemsHttpServiceProxy : OrdersDotComHttpProxyBase<InventoryItem, long>, IInventoryItemDataProxy
    {
        public InventoryItemsHttpServiceProxy(string baseAddress) :base(baseAddress) { }

        protected override string RequestUri
        {
            get { return $"{BaseAddress}/inventoryitems"; }
        }

        public InventoryItem GetByProduct(long productID)
        {
            return base.GET<InventoryItem>($"{RequestUri}?productid={productID}");
        }

        public Task<InventoryItem> GetByProductAsync(long productID)
        {
            return base.GETAsync<InventoryItem>($"{RequestUri}?productid={productID}");
        }
    }
}
