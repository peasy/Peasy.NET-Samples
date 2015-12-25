using System.Web.Mvc;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using Orders.com.Web.MVC.ViewModels;

namespace Orders.com.Web.MVC.Controllers
{
    public class OrdersController : ControllerBase<Order, OrderViewModel>
    {
        public OrdersController(IOrderService service) : base(service)
        {
        }

        public override ActionResult Index(string search)
        {
            var service = _service as IOrderService;
            var results = service.GetAllCommand(0, 100).Execute().Value;
            return View(results);
        }
    }
}