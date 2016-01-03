using Orders.com.Web.MVC.ViewModels;
using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class CustomersController : ControllerBase<Customer, CustomerViewModel>
    {
        public CustomersController(ICustomerService service) : base(service)
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
                var customer = _service.GetByIDCommand(id).Execute().Value;
                return HandleFailedResult(new CustomerViewModel { Entity = customer }, result);
            }
        }
    }

    //public class CustomersIIController : Controller
    //{
    //    private ICustomerService _customerService;

    //    public CustomersIIController(ICustomerService customerDataProxy)
    //    {
    //        _customerService = customerDataProxy;
    //    }

    //    // GET: Customers
    //    public ActionResult Index(string search)
    //    {
    //        var customers = _customerService.GetAllCommand().Execute().Value;
    //        if (!string.IsNullOrEmpty(search))
    //            customers = customers.Where(c => c.Name.Contains(search));
    //        return View(customers);
    //    }

    //    // GET: Customers/Details/5
    //    public ActionResult Details(long? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Customer customer = _customerService.GetByIDCommand(id.Value).Execute().Value;
    //        if (customer == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(customer);
    //    }

    //    // GET: Customers/Create
    //    public ActionResult Create()
    //    {
    //        return View(new CustomerViewModel { Customer = new Customer() });
    //    }

    //    // POST: Customers/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Name")] Customer customer)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var result = _customerService.InsertCommand(customer).Execute();
    //            if (result.Success)
    //            {
    //                return RedirectToAction("Index");
    //            }
    //            else
    //                return View(new CustomerViewModel { Customer = customer, Errors = result.Errors });
    //        }

    //        return View(new CustomerViewModel { Customer = customer });
    //    }

    //    // GET: Customers/Edit/5
    //    public ActionResult Edit(long? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Customer customer = _customerService.GetByIDCommand(id.Value).Execute().Value;
    //        if (customer == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(customer);
    //    }

    //    // POST: Customers/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "ID, Name")] Customer customer, string Test)
    //    {
    //        var original = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(Test);
    //        var properties = customer.GetType().GetProperties();
    //        foreach (var property in properties)
    //        {
    //            if (property.GetValue(original)?.ToString() != property.GetValue(customer)?.ToString())
    //            {
    //                System.Diagnostics.Debug.WriteLine($"{property.Name} has changed");
    //            }
    //        }
    //        var result = _customerService.UpdateCommand(customer).Execute();
    //        if (result.Success)
    //            return RedirectToAction("Index");

    //        return View(customer);
    //    }

    //    // GET: Customers/Delete/5
    //    public ActionResult Delete(long? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Customer customer = _customerService.GetByIDCommand(id.Value).Execute().Value;
    //        if (customer == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(customer);
    //    }

    //    // POST: Customers/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(long id)
    //    {
    //        _customerService.DeleteCommand(id).Execute();
    //        return RedirectToAction("Index");
    //    }
    //}
}
