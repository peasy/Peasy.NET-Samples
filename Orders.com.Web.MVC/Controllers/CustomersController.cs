using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Orders.com.DAL.EF;
using Orders.com.Domain;

namespace Orders.com.Web.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private CustomersRepository _customers = new CustomersRepository();

        // GET: Customers
        public ActionResult Index()
        {
            return View(_customers.GetAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,Name,Self,CreatedBy,CreatedDatetime,LastModifiedBy,LastModifiedDatetime")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customers.Insert(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer, string Test)
        //public ActionResult Edit([Bind(Include = "ID,CustomerID,Name,Self,CreatedBy,CreatedDatetime,LastModifiedBy,LastModifiedDatetime")] Customer customer)
        {
            var original = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(Test);
            var properties = customer.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(original)?.ToString() != property.GetValue(customer)?.ToString())
                {
                    System.Diagnostics.Debug.WriteLine($"{property.Name} has changed");
                }
            }
            if (ModelState.IsValid)
            {
                customer = _customers.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _customers.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
