using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.DAL.EF
{
    public class CategoryRepository : OrdersDotComRepositoryBase<Category>, ICategoryDataProxy
    {
    }
}
