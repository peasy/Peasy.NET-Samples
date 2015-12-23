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

        //// GET: Entities
        public virtual ActionResult Index(string search)
        {
            var entities = _service.GetAllCommand().Execute().Value;
            //if (!string.IsNullOrEmpty(search))
            //    entities = customers.Where(c => c.Name.Contains(search));
            return View(entities);
        }

        //// GET: Entities/Details/5
        public virtual ActionResult Details(long? id)
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

        //// GET: Entities/Create
        public virtual ActionResult Create()
        {
            return View(new ViewModel<T> { Entity = default(T) });
        }

        //// POST: Entities/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T entity)
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

        //// GET: Entities/Edit/5
        public virtual ActionResult Edit(long? id)
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

        //// POST: Entities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(T entity, string Test)
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

        //// GET: Entities/Delete/5
        public virtual ActionResult Delete(long? id)
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

        //// POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
            var result = _service.DeleteCommand(id).Execute();
            if (result.Success)
                return RedirectToAction("Index");
            else
                return View(new ViewModel<T> { Errors = result.Errors });
        }
    }
}