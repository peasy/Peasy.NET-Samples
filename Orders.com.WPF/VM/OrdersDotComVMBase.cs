using Orders.com.BLL;
using Orders.com;
using Orders.com.BLL.Services;
using Orders.com.BLL.Domain;

namespace Orders.com.WPF.VM
{
    public abstract class OrdersDotComVMBase<T> : EntityViewModelBase<T, long> where T : DomainBase, new()
    {
        public OrdersDotComVMBase(OrdersDotComServiceBase<T> service) : base(service)
        {
        }

        public OrdersDotComVMBase(T entity, OrdersDotComServiceBase<T> service) : base(entity, service)
        {
        }
    }
}
