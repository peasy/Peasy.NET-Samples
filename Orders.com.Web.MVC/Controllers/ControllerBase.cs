using Orders.com.BLL.Domain;
using Orders.com.Web.MVC.ViewModels;
using Peasy;
using Peasy.Extensions;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Orders.com.Web.MVC.Controllers
{
    public class ControllerBase<T, TViewModel> : Controller where T : DomainBase, new()
                                                            where TViewModel : ViewModel<T>, new()
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
            var viewModels = entities.Select(e => new TViewModel() { Entity = e }).ToArray();
            viewModels.ForEach(TViewModel => ConfigureVM(TViewModel));
            return View(viewModels);
        }

        //// GET: Entities/Create
        public virtual ActionResult Create()
        {
            var vm = new TViewModel() { Entity = new T() };
            return View(ConfigureVM(vm));
        }

        //// POST: Entities/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TViewModel vm)
        {
            var result = _service.InsertCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Index");
            else
                return HandleFailedResult(vm, result);
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
            var vm = new TViewModel() { Entity = entity };
            return View(ConfigureVM(vm));
        }

        //// POST: Entities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TViewModel vm)
        {
            var result = _service.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
                return RedirectToAction("Index");
            else
                return HandleFailedResult(vm, result);
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
            var vm = new TViewModel() { Entity = entity };
            return View(ConfigureVM(vm));
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
                return HandleFailedResult(ConfigureVM(new TViewModel()), result);
        }

        protected ActionResult HandleFailedResult(TViewModel vm, ExecutionResult result)
        {
            result.Errors.ForEach(error =>
            {
                string member = error.MemberNames.First() ?? string.Empty;
                ModelState.AddModelError(member, error.ErrorMessage);
            });

            return View(ConfigureVM(vm));
        }

        protected virtual TViewModel ConfigureVM(TViewModel vm)
        {
            return vm;
        }
    }
}