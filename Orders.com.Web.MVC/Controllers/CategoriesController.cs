using Orders.com.Web.MVC.ViewModels;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;

namespace Orders.com.Web.MVC.Controllers
{
    public class CategoriesController : ControllerBase<Category, CategoryViewModel>
    {
        public CategoriesController(ICategoryService service) : base(service)
        {
        }
    }
}
