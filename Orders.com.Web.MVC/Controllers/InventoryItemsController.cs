using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;
using Peasy.Exception;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class InventoryItemsController : ControllerBase<InventoryItem, InventoryItemViewModel>
    {
        private IProductService _productService;
        private IEnumerable<Product> _products;

        public InventoryItemsController(IInventoryItemService service, IProductService productService) : base(service)
        {
            _productService = productService;
        }

        protected override InventoryItemViewModel ConfigureVM(InventoryItemViewModel vm)
        {
            vm.ProductName = Products.First(p => p.ProductID == vm.ProductID).Name;
            return vm;
        }

        public override ActionResult Edit(InventoryItemViewModel vm)
        {
            try
            {
                return base.Edit(vm);
            }
            catch (ConcurrencyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ConfigureVM(vm));
            }
        }

        private IEnumerable<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = _productService.GetAllCommand().Execute().Value;
                }
                return _products;
            }
        }

        [HttpGet]
        public ActionResult Product(int id)
        {
            var service = _service as IInventoryItemService;
            var inventoryItem = service.GetByProductCommand(id).Execute().Value;
            return Json(inventoryItem, JsonRequestBehavior.AllowGet);
        }
    }
}