using System.Collections.Generic;
using Orders.com.BLL.Domain;
using Peasy;

namespace Orders.com.BLL.Services
{
    public interface IOrderItemService : IService<OrderItem, long>
    {
        ICommand<IEnumerable<OrderItem>> GetByOrderCommand(long orderID);
        ICommand<OrderItem> ShipCommand(long orderItemID);
        ICommand<OrderItem> SubmitCommand(long orderItemID);
    }
}