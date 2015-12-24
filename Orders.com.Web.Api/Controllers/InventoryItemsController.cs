using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using System.Web.Http;

namespace Orders.com.Web.Api.Controllers
{
    public class InventoryItemsController : ApiControllerBase<InventoryItem, long>
    {
        public InventoryItemsController(IInventoryItemService inventoryService)
        {
            _businessService = inventoryService;
        }

        [HttpGet]
        /// GET api/inventoryitems?productid=123
        public InventoryItem GetByProduct(long productID)
        {
            var item = (_businessService as IInventoryItemService).GetByProductCommand(productID).Execute().Value;
            return item;
        }
    }
}