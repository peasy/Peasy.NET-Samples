using Orders.com.DAL.EF.Entities;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class OrderItemRepository : OrdersDotComRepositoryBase<OrderItem, OrderItemEntity>, IOrderItemDataProxy
    {
        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            using (var context = GetDbContext())
            {
                return context.Set<OrderItem>().Where(i => i.OrderID == orderID).ToArray();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            using (var context = GetDbContext())
            {
                var items = await context.Set<OrderItem>().Where(i => i.OrderID == orderID).ToListAsync();
                return items;
            }
        }

        public OrderItem Ship(OrderItem orderItem)
        {
            return base.Update(orderItem);
        }

        public async Task<OrderItem> ShipAsync(OrderItem orderItem)
        {
            return await base.UpdateAsync(orderItem);
        }

        public OrderItem Submit(OrderItem orderItem)
        {
            return base.Update(orderItem);
        }

        public async Task<OrderItem> SubmitAsync(OrderItem orderItem)
        {
            return await base.UpdateAsync(orderItem);
        }
    }
}
