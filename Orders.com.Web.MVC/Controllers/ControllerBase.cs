using Orders.com.BLL;
using Orders.com.Domain;
using Orders.com.Web.MVC.ViewModels;
using Peasy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class CustomersController : ControllerBase<Customer>
    {
        public CustomersController(ICustomerService service) : base(service)
        {
        }
    }

    public class ControllerBase<T> : Controller where T : DomainBase
    {
        protected IService<T, long> _service;

        public ControllerBase(IService<T, long> service)
        {
            _service = service;
        }

        //// GET: Customers
        public ActionResult Index(string search)
        {
            var entities = _service.GetAllCommand().Execute().Value;
            //if (!string.IsNullOrEmpty(search))
            //    entities = customers.Where(c => c.Name.Contains(search));
            return View(entities);
        }

        //// GET: Customers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = _service.GetByIDCommand(id.Value).Execute().Value;
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        //// GET: Customers/Create
        public ActionResult Create()
        {
            return View(new ViewModel<T> { Entity = default(T) });
        }

        //// POST: Customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] T entity)
        {
            if (ModelState.IsValid)
            {
                var result = _service.InsertCommand(entity).Execute();
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(new ViewModel<T> { Entity = entity, Errors = result.Errors });
            }

            return View(new ViewModel<T> { Entity = entity });
        }

        //// GET: Customers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T entity = _service.GetByIDCommand(id.Value).Execute().Value;
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(new ViewModel<T> { Entity = entity });
        }

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T entity, string Test)
        {
            if (ModelState.IsValid)
            {
                var result = _service.UpdateCommand(entity).Execute();
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(new ViewModel<T> { Entity = entity, Errors = result.Errors });
            }

            return View(new ViewModel<T> { Entity = entity });
        }

        //// GET: Customers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T entity = _service.GetByIDCommand(id.Value).Execute().Value;
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(new ViewModel<T> { Entity = entity });
        }

        //// POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _service.DeleteCommand(id).Execute();
            return RedirectToAction("Index");
        }
    }
}