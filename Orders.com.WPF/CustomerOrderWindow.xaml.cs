using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.WPF.VM;
using System.Windows;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for CustomerOrderWindow.xaml
    /// </summary>
    public partial class CustomerOrderWindow
    {
        public CustomerOrderWindow()
        {
            InitializeComponent();
        }

        public CustomerOrderWindow(OrderService orderService,
                                   CustomerService customerService,
                                   OrderItemService orderItemService,
                                   InventoryItemService inventoryService,
                                   MainWindowVM mainVM,
                                   EventAggregator eventAggregator)
            : this()
        {
            var vm = new CustomerOrderVM(eventAggregator, orderService, orderItemService, inventoryService, mainVM);
            DataContext = vm;
        }

        public CustomerOrderWindow(OrderVM currentOrder,
                                   OrderService orderService,
                                   CustomerService customerService,
                                   OrderItemService orderItemService,
                                   InventoryItemService inventoryService,
                                   MainWindowVM mainVM,
                                   EventAggregator eventAggregator)
            : this()
        {
            var order = new Order()
            {
                ID = currentOrder.ID,
                CustomerID = currentOrder.CustomerID,
                OrderDate = currentOrder.OrderDate,
            };
            var vm = new CustomerOrderVM(eventAggregator, order, orderService, orderItemService, inventoryService, mainVM);
            vm.RefreshCommand.Execute(null);
            DataContext = vm;
        }

        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
