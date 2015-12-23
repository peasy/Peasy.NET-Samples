using Orders.com.BLL;
using Orders.com.Domain;
using Orders.com.Web.MVC.ViewModels;
using Peasy.Core;
using Peasy.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    //public class CustomersController : ControllerBase<Customer>
    //{
    //    public CustomersController(ICustomerService service) : base(service)
    //    {
    //    }
    //}

    public class ControllerBase<T, VM> : Controller where T : DomainBase, new()
                                                    where VM : ViewModel<T>, new()
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
            var viewModels = entities.Select(e => new VM() { Entity = e }).ToArray();
            viewModels.ForEach(vm => ConfigureVM(vm));
            return View(viewModels);
        }

        protected virtual void ConfigureVM(VM vm)
        {
        }

        //// GET: Entities/Create
        public virtual ActionResult Create()
        {
            var vm = new VM() { Entity = new T() };
            ConfigureVM(vm);
            return View(vm);
        }

        //// POST: Entities/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(VM vm)
        {
            var result = _service.InsertCommand(vm.Entity).Execute();
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                ConfigureVM(vm);
                return View(vm);
            }
        }

        //// GET: Entities/Edit/5
        public virtual ActionResult Edit(long? id)
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
            var vm = new VM() { Entity = entity };
            ConfigureVM(vm);
            return View(vm);
        }

        //// POST: Entities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(VM vm)
        {
            var result = _service.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                ConfigureVM(vm);
                return View(vm);
            }
        }

        //// GET: Entities/Delete/5
        public virtual ActionResult Delete(long? id)
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
            var vm = new VM() { Entity = entity };
            ConfigureVM(vm);
            return View(vm);
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
            {
                var vm = new VM();
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                ConfigureVM(vm);
                return View(vm);
            }
        }
    }
}