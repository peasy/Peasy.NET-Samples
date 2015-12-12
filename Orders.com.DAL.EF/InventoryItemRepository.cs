using System;
using System.Linq;
using System.Threading.Tasks;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Data.Entity;
using Orders.com.DAL.EF.Entities;

namespace Orders.com.DAL.EF
{
    public class InventoryItemRepository : OrdersDotComRepositoryBase<InventoryItem, InventoryItemEntity>, IInventoryItemDataProxy
    {
        public InventoryItem GetByProduct(long productID)
        {
            using (var context = GetDbContext())
            {
                return context.Set<InventoryItem>().First(i => i.ProductID == productID);
            }
        }

        public async Task<InventoryItem> GetByProductAsync(long productID)
        {
            using (var context = GetDbContext())
            {
                var items = await context.Set<InventoryItem>().Where(i => i.ProductID == productID).ToListAsync();
                return items.First();
            }
        }
    }
}
