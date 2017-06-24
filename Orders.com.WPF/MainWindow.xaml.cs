using Orders.com.BLL.Services;
using Orders.com.DAL.Http;
using Orders.com.DAL.InMemory;
using Orders.com.WPF.VM;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Orders.com.WPF
{
    public partial class MainWindow
    {
        private CustomerService _customersService;
        private OrderService _ordersService;
        private OrderItemService _orderItemsService;
        private ProductService _productsService;
        private CategoryService _categoriesService;
        private EventAggregator _eventAggregator = new EventAggregator();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //ConfigureInMemoryUsage();
            ConfigureHttpClientUsage();
            //ConfigureEFUsage();
        }

        private void ConfigureHttpClientUsage()
        {
            string hostSettingName = "apiHostNameAddress";
            var baseAddress = ConfigurationManager.AppSettings[hostSettingName];
            if (baseAddress == null) throw new Exception($"The setting '{hostSettingName}' in the AppSettings portion of the configuration file was not found.");

            var productsDataProxy = new ProductsHttpServiceProxy(baseAddress);
            var inventoryDataProxy = new InventoryItemsHttpServiceProxy(baseAddress);
            var customerDataProxy = new CustomersHttpServiceProxy(baseAddress);
            var orderItemDataProxy = new OrderItemsHttpServiceProxy(baseAddress);
            var orderRepository = new OrdersHttpServiceProxy(baseAddress);
            var categoriesDataProxy = new CategoriesHttpServiceProxy(baseAddress);

            _inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemClientService(orderItemDataProxy, productsDataProxy, inventoryDataProxy, new DTCTransactionContext());
            _ordersService = new OrderService(orderRepository, _orderItemsService, new DTCTransactionContext());
            _customersService = new CustomerService(customerDataProxy, _ordersService);
            _productsService = new ProductClientService(productsDataProxy, orderRepository, _inventoryService, new DTCTransactionContext());
            _categoriesService = new CategoryService(categoriesDataProxy, productsDataProxy);
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, _ordersService, _inventoryService);
        }

        private void ConfigureInMemoryUsage()
        {
            var productsDataProxy = new ProductRepository();
            var inventoryDataProxy = new InventoryItemRepository();
            var customerDataProxy = new CustomerRepository();
            var orderItemDataProxy = new OrderItemRepository();
            var orderRepository = new OrderRepository(customerDataProxy, orderItemDataProxy);
            var categoriesDataProxy = new CategoryRepository();

            _inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemService(orderItemDataProxy, productsDataProxy, inventoryDataProxy, new DTCTransactionContext());
            _ordersService = new OrderService(orderRepository, _orderItemsService, new DTCTransactionContext());
            _customersService = new CustomerService(customerDataProxy, _ordersService);
            _productsService = new ProductService(productsDataProxy, orderRepository, _inventoryService, new DTCTransactionContext());
            _categoriesService = new CategoryService(categoriesDataProxy, productsDataProxy);
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, _ordersService, _inventoryService);
        }

        private void ConfigureEFUsage()
        {
            var productsDataProxy = new DAL.EF.ProductRepository();
            var inventoryDataProxy = new DAL.EF.InventoryItemRepository();
            var customerDataProxy = new DAL.EF.CustomerRepository();
            var orderItemDataProxy = new DAL.EF.OrderItemRepository();
            var orderRepository = new DAL.EF.OrderRepository();
            var categoriesDataProxy = new DAL.EF.CategoryRepository();

            _inventoryService = new InventoryItemService(inventoryDataProxy);
            _orderItemsService = new OrderItemService(orderItemDataProxy, productsDataProxy, inventoryDataProxy, new DTCTransactionContext());
            _ordersService = new OrderService(orderRepository, _orderItemsService, new DTCTransactionContext());
            _customersService = new CustomerService(customerDataProxy, _ordersService);
            _productsService = new ProductService(productsDataProxy, orderRepository, _inventoryService, new DTCTransactionContext());
            _categoriesService = new CategoryService(categoriesDataProxy, productsDataProxy);
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, _ordersService, _inventoryService);
        }

        public MainWindowVM VM
        {
            get { return this.DataContext as MainWindowVM; }
        }

        public InventoryItemService _inventoryService { get; private set; }

        private void addCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var customerOrderWindow = new CustomerOrderWindow(_ordersService, _customersService, _orderItemsService, _inventoryService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            customerOrderWindow.Show();
        }

        private void editCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var currentOrder = VM.OrdersVM.SelectedOrder;
            var customerOrderWindow = new CustomerOrderWindow(currentOrder, _ordersService, _customersService, _orderItemsService, _inventoryService, VM, _eventAggregator);
            customerOrderWindow.Owner = this;
            customerOrderWindow.Show();
        }

        private void InventoryTab_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source is TabItem)
                VM.InventoryItemsVM.LoadInventoryCommand.Execute(null);
        }
    }
}
