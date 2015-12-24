using System.Collections.Generic;
using Peasy.Core;
using Orders.com.BLL.Domain;

namespace Orders.com.BLL.Services
{
    public interface IOrderItemService : IService<OrderItem, long>
    {
        ICommand<IEnumerable<OrderItem>> GetByOrderCommand(long orderID);
        ICommand<OrderItem> ShipCommand(long orderItemID);
        ICommand<OrderItem> SubmitCommand(long orderItemID);
    }
}