using Orders.com.Domain;
using Orders.com.DataProxy;
using Orders.com.DAL.EF.Entities;

namespace Orders.com.DAL.EF
{
    public class CustomerRepository : OrdersDotComRepositoryBase<Customer, CustomerEntity>, ICustomerDataProxy
    {
    }
}
