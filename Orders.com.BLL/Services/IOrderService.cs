using System.Collections.Generic;
using Orders.com.BLL.Domain;
using Orders.com.BLL.QueryData;
using Peasy;

namespace Orders.com.BLL.Services
{
    public interface IOrderService : IService<Order, long>
    {
        ICommand<IEnumerable<OrderInfo>> GetAllCommand(int start, int pageSize);
        ICommand<IEnumerable<Order>> GetByCustomerCommand(long customerID);
        ICommand<IEnumerable<Order>> GetByProductCommand(long productID);
    }
}