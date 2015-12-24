using Orders.com.BLL.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Orders.com.Web.MVC.ViewModels
{
    public class ProductViewModel : ViewModel<Product>
    {
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
            get; set;
        }

        public Category AssociatedCategory
        {
            get
            {
                return Categories.FirstOrDefault(c => c.CategoryID == Entity.CategoryID);
            }
        }

        //public bool Save()
        //{
        //    var result = _service.InsertCommand(Entity).Execute();
        //    if (result.Success)
        //    {
        //        Entity = result.Value;
        //        return true;
        //    }
        //    else
        //    {
        //        Errors = result.Errors;
        //        return false;
        //    }
        //}
    }
}