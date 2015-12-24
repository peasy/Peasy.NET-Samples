using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.BLL.Services
{
    public class OrderStatusService : OrdersDotComServiceBase<OrderStatus>, IOrderStatusService
    {
        public OrderStatusService(IOrderStatusDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
