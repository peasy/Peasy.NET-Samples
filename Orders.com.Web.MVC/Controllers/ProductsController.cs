using Orders.com.Domain;
using Orders.com.BLL;
using Orders.com.Web.MVC.ViewModels;
using System.Collections.Generic;

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
    }
}
