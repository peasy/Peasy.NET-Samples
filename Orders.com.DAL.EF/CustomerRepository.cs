using Orders.com.Domain;
using Orders.com.DataProxy;

namespace Orders.com.DAL.EF
{
    public class CustomerRepository : OrdersDotComRepositoryBase<Customer>, ICustomerDataProxy
    {
    }
}
