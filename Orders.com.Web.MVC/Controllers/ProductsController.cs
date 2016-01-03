using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class ProductsController : ControllerBase<Product, ProductViewModel>
    {
        private readonly ICategoryService _categoryService;
        private IEnumerable<Category> _categories;

        public ProductsController(IProductService service, ICategoryService categoryService) : base(service)
        {
            _categoryService = categoryService;
        }

        protected override ProductViewModel ConfigureVM(ProductViewModel vm)
        {
            vm.Categories = Categories;
            return vm;
        }

        protected IEnumerable<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = _categoryService.GetAllCommand().Execute().Value;
                }
                return _categories;
            }
        }

        [HttpGet]
        public ActionResult Category(long id)
        {
            var service = _service as IProductService;
            var products = service.GetByCategoryCommand(id).Execute().Value;
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Product(long id)
        {
            var product = _service.GetByIDCommand(id).Execute().Value;
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public override ActionResult DeleteConfirmed(long id)
        {
            var result = _service.DeleteCommand(id).Execute();
            if (result.Success)
                return RedirectToAction("Index");
            else
            {
                var product = _service.GetByIDCommand(id).Execute().Value;
                return HandleFailedResult(new ProductViewModel { Entity = product }, result);
            }
        }
    }
}
