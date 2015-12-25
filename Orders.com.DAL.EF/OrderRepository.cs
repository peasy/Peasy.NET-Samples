using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.com.BLL.Extensions;
using System.Data.Entity;
using Orders.com.BLL.Domain;
using Orders.com.BLL.QueryData;
using Orders.com.BLL.DataProxy;

namespace Orders.com.DAL.EF
{
    public class OrderRepository : OrdersDotComRepositoryBase<Order>, IOrderDataProxy
    {
        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            using (var context = GetDbContext())
            {
                var results = context.Set<Order>()
                                    .Join(context.Set<Customer>(),
                                          o => o.CustomerID,
                                          c => c.ID,
                                        (o, c) => new { Order = o, Customer = c })
                                    .GroupJoin(context.Set<OrderItem>(),
                                          o => o.Order.ID,
                                          oi => oi.OrderID,
                                        (o, oi) => new { Order = o.Order, Customer = o.Customer, OrderItems = oi })
                                    .OrderBy(o => o.Order.ID)
                                    .Skip(start)
                                    .Take(pageSize)
                                    .Select(o => new
                                    {
                                        OrderID = o.Order.ID,
                                        OrderDate = o.Order.OrderDate,
                                        CustomerName = o.Customer.Name,
                                        CustomerID = o.Customer.ID,
                                        OrderItems = o.OrderItems
                                    })
                                    .ToArray()
                                    .Select(o => new OrderInfo()
                                    {
                                        OrderID = o.OrderID,
                                        OrderDate = o.OrderDate,
                                        CustomerName = o.CustomerName,
                                        CustomerID = o.CustomerID,
                                        Total = o.OrderItems.Sum(i => i.Amount),
                                        Status = BuildStatusName(o.OrderItems),
                                        HasShippedItems = o.OrderItems.Any(i => i.OrderStatus() is ShippedState)
                                    });

                return results;
            }
        }

        private static string BuildStatusName(IEnumerable<OrderItem> orderItems)
        {
            if (!orderItems.Any()) return string.Empty;
            return orderItems.OrderStatus().Name;
        }

        public async Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize)
        {
            using (var context = GetDbContext() as OrdersDotComContext)
            {
                var results = await context.Set<Order>()
                                    .Join(context.Set<Customer>(),
                                          o => o.CustomerID,
                                          c => c.ID,
                                        (o, c) => new { Order = o, Customer = c })
                                    .GroupJoin(context.Set<OrderItem>(),
                                          o => o.Order.ID,
                                          oi => oi.OrderID,
                                        (o, oi) => new { Order = o.Order, Customer = o.Customer, OrderItems = oi })
                                    .OrderBy(o => o.Order.ID)
                                    .Skip(start)
                                    .Take(pageSize)
                                    .Select(o => new
                                    {
                                        OrderID = o.Order.ID,
                                        OrderDate = o.Order.OrderDate,
                                        CustomerName = o.Customer.Name,
                                        CustomerID = o.Customer.ID,
                                        OrderItems = o.OrderItems
                                    }).ToListAsync();

                return results.Select(o => new OrderInfo()
                                     {
                                         OrderID = o.OrderID,
                                         OrderDate = o.OrderDate,
                                         CustomerName = o.CustomerName,
                                         CustomerID = o.CustomerID,
                                         Total = o.OrderItems.Sum(i => i.Amount),
                                         Status = BuildStatusName(o.OrderItems),
                                         HasShippedItems = o.OrderItems.Any(i => i.OrderStatus() is ShippedState)
                                     });

            }
        }

        public IEnumerable<Order> GetByCustomer(long customerID)
        {
            using (var context = GetDbContext())
            {
                return context.Set<Order>().Where(o => o.CustomerID == customerID).ToList();
            }
        }

        public async Task<IEnumerable<Order>> GetByCustomerAsync(long customerID)
        {
            using (var context = GetDbContext())
            {
                return await context.Set<Order>().Where(o => o.CustomerID == customerID).ToListAsync();
            }
        }

        public IEnumerable<Order> GetByProduct(long productID)
        {
            using (var context = GetDbContext())
            {
                return context.Set<Order>()
                              .GroupJoin(context.Set<OrderItem>(),
                                         o => o.ID,
                                         oi => oi.OrderID,
                                         (o, oi) => new { Order = o, Items = oi })
                              .Where(o => o.Items.Any(i => i.ProductID == productID))
                              .Select(o => o.Order)
                              .ToArray();
            }
        }

        public async Task<IEnumerable<Order>> GetByProductAsync(long productID)
        {
            using (var context = GetDbContext())
            {
                return await context.Set<Order>()
                              .GroupJoin(context.Set<OrderItem>(),
                                         o => o.ID,
                                         oi => oi.OrderID,
                                         (o, oi) => new { Order = o, Items = oi })
                              .Where(o => o.Items.Any(i => i.ProductID == productID))
                              .Select(o => o.Order)
                              .ToListAsync();
            }
        }
    }
}
