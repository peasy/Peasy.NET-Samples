using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;

namespace Orders.com.Web.Api.Controllers
{
    public class CategoriesController : ApiControllerBase<Category, long>
    {
        public CategoriesController(ICategoryService categoryService)
        {
            _businessService = categoryService;
        }
    }
}