using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.DAL.Http
{
    public class CustomersHttpServiceProxy : OrdersDotComHttpProxyBase<Customer, long>, ICustomerDataProxy
    {
        protected override string RequestUri
        {
            get { return $"{BaseAddress}/customers"; }
        }
    }
}
