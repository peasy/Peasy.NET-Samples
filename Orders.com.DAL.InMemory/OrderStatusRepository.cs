using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;
using System.Collections.Generic;

namespace Orders.com.DAL.InMemory
{
    public class OrderStatusRepository : OrdersDotComRepositoryBase<OrderStatus>, IOrderStatusDataProxy
    {
        protected override IEnumerable<OrderStatus> SeedDataProxy()
        {
            yield return new OrderStatus() { OrderStatusID = 1, Name = "Pending" };
            yield return new OrderStatus() { OrderStatusID = 2, Name = "Submitted" };
            yield return new OrderStatus() { OrderStatusID = 3, Name = "Shipped" };
        }
    }
}
