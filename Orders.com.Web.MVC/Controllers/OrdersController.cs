using System.Web.Mvc;
using System.Linq;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;
using Peasy.Core.Extensions;

namespace Orders.com.Web.MVC.Controllers
{
    public class OrdersController : ControllerBase<Order, OrderViewModel>
    {
        private ICustomerService _customerService;
        private IOrderItemService _orderItemService;
        private IEnumerable<Customer> _customers;
        private IEnumerable<OrderItem> _orderItems;
        private IProductService _productService;
        private IEnumerable<Product> _products;

        public OrdersController(IOrderService service, 
                                IOrderItemService orderItemService, 
                                ICustomerService customerService, 
                                IProductService productService) : base(service)
        {
            _orderItemService = orderItemService;
            _customerService = customerService;
            _productService = productService;
        }

        public override ActionResult Index(string search)
        {
            var service = _service as IOrderService;
            var results = service.GetAllCommand(0, 100).Execute().Value;
            return View(results);
        }

        protected override OrderViewModel ConfigureVM(OrderViewModel vm)
        {
            vm.Customers = Customers;
            vm.OrderItems = OrderItems(vm.ID)
                              .Select(i => new OrderItemViewModel { Entity = i })
                              .ToArray()
                              .ForEach(oi => oi.ProductName = Products.First(p => p.ProductID == oi.ProductID).Name);
            return vm;
        }

        private IEnumerable<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = _customerService.GetAllCommand().Execute().Value;
                return _customers;
            }
        }

        private IEnumerable<Product> Products
        {
            get
            {
                if (_products == null)
                    _products = _productService.GetAllCommand().Execute().Value;
                return _products;
            }
        }

        private IEnumerable<OrderItem> OrderItems(long orderID)
        {
            if (_orderItems == null)
                _orderItems = _orderItemService.GetByOrderCommand(orderID).Execute().Value;
            return _orderItems;
        }
    }
}