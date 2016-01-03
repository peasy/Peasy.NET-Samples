using Orders.com.Web.MVC.ViewModels;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class CategoriesController : ControllerBase<Category, CategoryViewModel>
    {
        public CategoriesController(ICategoryService service) : base(service)
        {
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
                var category = _service.GetByIDCommand(id).Execute().Value;
                return HandleFailedResult(new CategoryViewModel { Entity = category }, result);
            }
        }
    }
}
