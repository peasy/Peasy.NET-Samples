using Orders.com.BLL.Domain;
using Orders.com.BLL.Services;

namespace Orders.com.WPF.VM
{
    public class CustomerVM : OrdersDotComVMBase<Customer>
    {
        public CustomerVM (CustomerService service) : base(service)
        {
        }

        public CustomerVM(Customer customer, CustomerService service) : base(customer, service)
        {
        }

        public long ID
        {
            get { return CurrentEntity.ID; }
        }

        public string Name
        {
            get { return CurrentEntity.Name; }
            set
            {
                CurrentEntity.Name = value;
                IsDirty = true;
                OnPropertyChanged("Name");
            }
        }

        protected override void OnInsertSuccess(Customer result)
        {
            OnPropertyChanged("ID");
        }
    }
}
