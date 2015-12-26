using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;
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
            vm.InStock = _inventoryService.GetByProductCommand(vm.ProductID).Execute().Value.QuantityOnHand;
            return vm;
        }

        public override ActionResult Edit(OrderItemViewModel vm)
        {
            var result = _service.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Edit", "Orders", new { id = vm.OrderID });
            else
                return HandleFailedResult(vm, result);
        }
    }
}