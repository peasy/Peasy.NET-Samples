using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.com.QueryData;
using Orders.com.Extensions;
using System.Data.Entity;
using Orders.com.DAL.EF.Entities;

namespace Orders.com.DAL.EF
{
    public class OrderRepository : OrdersDotComRepositoryBase<Order, OrderEntity>, IOrderDataProxy
    {
        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            using (var context = GetDbContext())
            {
                var results = context.Set<Order>()
                                    .Join(context.Set<Customer>(),
                                          o => o.CustomerID,
                                          c => c.CustomerID,
                                        (o, c) => new { Order = o, Customer = c })
                                    .Skip(start)
                                    .Take(pageSize)
                                    .Select(o => new
                                    {
                                        OrderID = o.Order.OrderID,
                                        OrderDate = o.Order.OrderDate,
                                        CustomerName = o.Customer.Name,
                                        CustomerID = o.Customer.CustomerID,
                                        OrderItems = context.Set<OrderItem>().Where(i => i.OrderID == o.Order.OrderID)
                                    })
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
                //return context.Set<Order>()
                //              .Join(context.Set<OrderItem>(), 
                //                    o => o.OrderID, 
                //                    i => i.OrderID, 
                //                    (o, i) => new { Order = o, Item = i })
                //              .Join(context.Set<Customer>(), 
                //                    o => o.Order.CustomerID, 
                //                    c => c.CustomerID,
                //                    (o, c) => new { Order = o.Order, Item = o.Item, Customer = c })
                //              .Skip(start)
                //              .Take(pageSize)
                //              .Select(set => new OrderInfo
                //              {
                //                  CustomerID = set.Customer.ID,
                //                  CustomerName = set.Customer.Name,
                //                  HasShippedItems = set.Item.,
                //                  OrderDate = o.OrderDate,
                //                  OrderID = o.OrderID,
                //                  Status = 1,
                //                  Total = 45
                //              });
            }
        }

        private static string BuildStatusName(IEnumerable<OrderItem> orderItems)
        {
            if (!orderItems.Any()) return string.Empty;
            return orderItems.OrderStatus().Name;
        }

        public async Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize)
        {
            using (var context = GetDbContext())
            {
                context.Database.Log = Console.Write;
                var foo = GetDbContext() as OrdersDotComContext;
                foo.Orders.Join(foo.OrderItems, o => o.OrderID, c => c.OrderID, (o, c) => new { cust = c, order = o });
                foo.Orders.GroupJoin(foo.OrderItems, o => o.OrderID, oi => oi.OrderID, (o, oi) => new { order = o, items = oi });


                var results = await context.Set<OrderEntity>()
                                    .Include(o => o.Customer)
                                    .Include(o => o.OrderItems)
                                    .OrderBy(o => o.ID)
                                    .Skip(start)
                                    .Take(pageSize)
                                    .Select(o => new
                                    {
                                        OrderID = o.ID,
                                        OrderDate = o.OrderDate,
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
                    HasShippedItems = o.OrderItems.Any(i => i.OrderStatus() is ShippedState),
                }).ToArray();
            }
        }

        public IEnumerable<Order> GetByCustomer(long customerID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetByCustomerAsync(long customerID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetByProduct(long productID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetByProductAsync(long productID)
        {
            throw new NotImplementedException();
        }
    }
}
