using System.Collections.Generic;
using Peasy.Core;
using Orders.com.BLL.Domain;

namespace Orders.com.BLL.Services
{
    public interface IProductService : IService<Product, long>
    {
        ICommand<IEnumerable<Product>> GetByCategoryCommand(long categoryID);
    }
}