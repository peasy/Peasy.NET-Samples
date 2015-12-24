using Orders.com.BLL.Domain;
using Orders.com.BLL.QueryData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.BLL.DataProxy
{
    public interface IOrderDataProxy : IOrdersDotComDataProxy<Order>
    {
        IEnumerable<OrderInfo> GetAll(int start, int pageSize);
        Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize);
        IEnumerable<Order> GetByCustomer(long customerID);
        Task<IEnumerable<Order>> GetByCustomerAsync(long customerID);
        IEnumerable<Order> GetByProduct(long productID);
        Task<IEnumerable<Order>> GetByProductAsync(long productID);
    }
}
