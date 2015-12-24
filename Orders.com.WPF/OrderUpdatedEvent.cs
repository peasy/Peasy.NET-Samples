using Orders.com.WPF.VM;

namespace Orders.com.WPF
{
    public class OrderUpdatedEvent
    {
        public OrderUpdatedEvent(CustomerOrderVM order)
        {
            Order = order;
        }

        public CustomerOrderVM Order { get; private set; }
    }
}
