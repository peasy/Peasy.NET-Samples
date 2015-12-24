using Orders.com.BLL.Domain;
using System;

namespace Orders.com.BLL.Extensions
{
    public static class OrderItemExtensions
    {
        public static Tuple<OrderItem> SetPrice(this OrderItem item, decimal price)
        {
            item.Price = price;
            return new Tuple<OrderItem>(item);
        }

        public static OrderItem SetAmount(this OrderItem item)
        {
            item.Amount = item.Quantity * item.Price;
            return item;
        }
    }
}
