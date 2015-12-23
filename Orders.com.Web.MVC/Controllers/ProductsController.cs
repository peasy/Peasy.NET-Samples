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
using Orders.com.BLL;
using Orders.com.Web.MVC.ViewModels;
using Peasy.Core.Extensions;

namespace Orders.com.Web.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductsController(IProductService service, ICategoryService categoryService)
        {
            _productService = service;
            _categoryService = categoryService;
        }

        public ActionResult Index(string search)
        {
            var products = _productService.GetAllCommand().Execute().Value;
            var viewModels = products.Select(p => new ProductViewModel(p, _productService, _categoryService));
            return View(viewModels);
        }


        public ActionResult Create()
        {
            return View(new ProductViewModel(new Product(), _productService, _categoryService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vm)
        {
            var result = _productService.InsertCommand(vm.Entity).Execute();
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                return View(vm.Use(_categoryService));
            }
        }

        public virtual ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = _productService.GetByIDCommand(id.Value).Execute().Value;
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(new ProductViewModel(entity, _productService, _categoryService));
        }

        //// POST: Entities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ProductViewModel vm, string Test)
        {
            var result = _productService.UpdateCommand(vm.Entity).Execute();
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                return View(vm.Use(_categoryService));
            }
        }

        public virtual ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = _productService.GetByIDCommand(id.Value).Execute().Value;
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(new ProductViewModel(entity, _productService, _categoryService));
        }

        //// POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
            var result = _productService.DeleteCommand(id).Execute();
            if (result.Success)
                return RedirectToAction("Index");
            else
            {
                var vm = new ProductViewModel();
                vm.Errors = result.Errors;
                vm.Errors.ForEach(e => ModelState.AddModelError(e.MemberNames.First(), e.ErrorMessage));
                return View(vm.Use(_categoryService));
            }
        }
    }
}
