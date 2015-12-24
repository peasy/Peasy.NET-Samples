using Orders.com.WPF.VM;

namespace Orders.com.WPF
{
    public class OrderInsertedEvent
    {
        public CustomerOrderVM Order { get; set; }
    }
}
