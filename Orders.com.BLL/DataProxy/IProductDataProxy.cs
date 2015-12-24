using Orders.com.BLL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.BLL.DataProxy
{
    public interface IProductDataProxy : IOrdersDotComDataProxy<Product>
    {
        IEnumerable<Product> GetByCategory(long categoryID);
        Task<IEnumerable<Product>> GetByCategoryAsync(long categoryID);
    }
}
