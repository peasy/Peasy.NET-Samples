using Orders.com.BLL.Domain;

namespace Orders.com.Web.MVC.ViewModels
{
    public class CustomerViewModel : ViewModel<Customer>
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
    }
}