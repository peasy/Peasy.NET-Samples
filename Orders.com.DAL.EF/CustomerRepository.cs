using Orders.com.BLL.Domain;
using Orders.com.BLL.DataProxy;

namespace Orders.com.DAL.EF
{
    public class CustomerRepository : OrdersDotComRepositoryBase<Customer>, ICustomerDataProxy
    {
    }
}
