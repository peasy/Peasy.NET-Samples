using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Peasy.Exception;
using Orders.com.BLL.Domain;
using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Extensions;

namespace Orders.com.DAL.EF
{
    public class InventoryItemRepository : OrdersDotComRepositoryBase<InventoryItem>, IInventoryItemDataProxy
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

        protected override void OnBeforeUpdateExecuted(DbContext context, InventoryItem entity)
        {
            var existing = context.Set<InventoryItem>()
                                  .FirstOrDefault(e => e.ID.Equals(entity.ID) && e.Version == entity.Version);
            if (existing == null)
                throw new ConcurrencyException($"{entity.GetType().Name} with id {entity.ID.ToString()} was already changed");

            entity.IncrementVersionByOne();
        }

        protected override async Task OnBeforeUpdateExecutedAsync(DbContext context, InventoryItem entity)
        {
            var existing = await context.Set<InventoryItem>()
                                        .FirstOrDefaultAsync(e => e.ID.Equals(entity.ID) && e.Version == entity.Version);
            if (existing == null)
                throw new ConcurrencyException($"{entity.GetType().Name} with id {entity.ID.ToString()} was already changed");

            entity.IncrementVersionByOne();
        }
    }
}