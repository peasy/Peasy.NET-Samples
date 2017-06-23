using System.Web.Mvc;
using System.Linq;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;
using Peasy.Extensions;
using Orders.com.BLL.Extensions;

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

        [ValidateAntiForgeryToken]
        public override ActionResult Create(OrderViewModel vm)
        {
            var result = _service.InsertCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Edit", "Orders", new { id = result.Value.ID });
            else
                return HandleFailedResult(ConfigureVM(vm), result);
        }

        [ValidateAntiForgeryToken]
        public override ActionResult Edit(OrderViewModel vm)
        {
            var result = _service.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
                return View(ConfigureVM(vm));
            else
                return HandleFailedResult(ConfigureVM(vm), result);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Submit(long id)
        {
            var vm = new OrderViewModel { ID = id };
            var submittableItems = _orderItemService.GetByOrderCommand(id)
                                                  .Execute()
                                                  .Value
                                                  .Where(i => i.OrderStatus().CanSubmit);

            foreach (var item in submittableItems)
            {
                var result = _orderItemService.SubmitCommand(item.ID).Execute();
                if (!result.Success)
                    return HandleFailedResult(ConfigureVM(vm), result);
            }

            return RedirectToAction("Edit", "Orders", new { id = id });
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