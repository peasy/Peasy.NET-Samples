using Orders.com.BLL;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC.ViewModels
{
    public class ProductViewModel : ViewModel<Product>
    {
        private IProductService _service;
        private ICategoryService _categoryService;
        private IEnumerable<Category> _categories;

        public ProductViewModel()
        {
            Entity = new Product();
        }

        public ProductViewModel(Product product, IProductService service, ICategoryService categoryService)
        {
            Entity = product;
            _service = service;
            _categoryService = categoryService;
        }

        public long ID
        {
            get { return Entity.ID; }
            set { Entity.ID = value; }
        }

        public string Name
        {
            get { return Entity.Name; }
            set { Entity.Name = value; }
        }

        public string Description
        {
            get { return Entity.Description; }
            set { Entity.Description = value; }
        }

        public long CategoryID
        {
            get { return Entity.CategoryID; }
            set { Entity.CategoryID = value; }
        }

        public decimal? Price
        {
            get { return Entity.Price; }
            set { Entity.Price = value; }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = _categoryService.GetAllCommand().Execute().Value;
                }
                return _categories;
            }
        }

        public ProductViewModel Use(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            return this;
        }

        public Category AssociatedCategory
        {
            get
            {
                return Categories.FirstOrDefault(c => c.CategoryID == Entity.CategoryID);
            }
        }

        public bool Save()
        {
            var result = _service.InsertCommand(Entity).Execute();
            if (result.Success)
            {
                Entity = result.Value;
                return true;
            }
            else
            {
                Errors = result.Errors;
                return false;
            }
        }
    }
}