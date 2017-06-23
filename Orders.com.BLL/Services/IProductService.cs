using System.Collections.Generic;
using Orders.com.BLL.Domain;
using Peasy;

namespace Orders.com.BLL.Services
{
    public interface IProductService : IService<Product, long>
    {
        ICommand<IEnumerable<Product>> GetByCategoryCommand(long categoryID);
    }
}