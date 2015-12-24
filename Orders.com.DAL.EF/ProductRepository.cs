using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class ProductRepository : OrdersDotComRepositoryBase<Product>, IProductDataProxy
    {
        public IEnumerable<Product> GetByCategory(long categoryID)
        {
            using (var context = GetDbContext())
            {
                return context.Set<Product>().Where(i => i.CategoryID == categoryID).ToList();
            }
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(long categoryID)
        {
            using (var context = GetDbContext())
            {
                return await context.Set<Product>().Where(i => i.CategoryID == categoryID).ToListAsync();
            }
        }
    }
}
