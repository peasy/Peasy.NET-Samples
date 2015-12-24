using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;

namespace Orders.com.Web.Api.Controllers
{
    public class CustomersController : ApiControllerBase<Customer, long>
    {
        public CustomersController(ICustomerService customerService)
        {
            _businessService = customerService;
        }
    }
}