using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class OrderItemsController : ControllerBase<OrderItem, OrderItemViewModel>
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private IInventoryItemService _inventoryService;

        public OrderItemsController(IOrderItemService service,
                                    ICategoryService categoryService,
                                    IProductService productService,
                                    IInventoryItemService inventoryService) : base(service)
        {
            _categoryService = categoryService;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        protected override OrderItemViewModel ConfigureVM(OrderItemViewModel vm)
        {
            vm.Products = _productService.GetAllCommand().Execute().Value;
            vm.Categories = _categoryService.GetAllCommand().Execute().Value;
            if (vm.ProductID > 0)
            {
                vm.InStock = _inventoryService.GetByProductCommand(vm.ProductID).Execute().Value.QuantityOnHand;
                vm.AssociatedProduct = vm.Products.First(p => p.ProductID == vm.ProductID);
            }
            return vm;
        }

        [HttpGet]
        public ActionResult Create(long orderID)
        {
            var vm = new OrderItemViewModel { Entity = new OrderItem { OrderID = orderID } };
            return View(ConfigureVM(vm));
        }

        public override ActionResult Create(OrderItemViewModel vm)
        {
            var result = _service.InsertCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Edit", "Orders", new { id = vm.OrderID });
            else
                return HandleFailedResult(vm, result);
        }

        public override ActionResult Edit(OrderItemViewModel vm)
        {
            var result = _service.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Edit", "Orders", new { id = vm.OrderID });
            else
                return HandleFailedResult(vm, result);
        }

        [HttpPost]
        public ActionResult Submit(long id, long orderID)
        {
            return RedirectToAction("Edit", "Orders", new { id = orderID });
        }
    }
}