using Orders.com.Domain;
using Orders.com.BLL;
using Orders.com.Web.MVC.ViewModels;

namespace Orders.com.Web.MVC.Controllers
{
    public class CategoriesController : ControllerBase<Category, CategoryViewModel>
    {
        public CategoriesController(ICategoryService service) : base(service)
        {
        }
    }
}
