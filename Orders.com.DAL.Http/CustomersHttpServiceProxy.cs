using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.DAL.Http
{
    public class CustomersHttpServiceProxy : OrdersDotComHttpProxyBase<Customer, long>, ICustomerDataProxy
    {
        public CustomersHttpServiceProxy(string baseAddress) :base(baseAddress) { }

        protected override string RequestUri
        {
            get { return $"{BaseAddress}/customers"; }
        }
    }
}
