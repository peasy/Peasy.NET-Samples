using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;
using System.Collections.Generic;

namespace Orders.com.DAL.InMemory
{
    public class CustomerRepository : OrdersDotComRepositoryBase<Customer>, ICustomerDataProxy
    {
        protected override IEnumerable<Customer> SeedDataProxy()
        {
            yield return new Customer() { CustomerID = 1, Name = "Jimi Hendrix" };
            yield return new Customer() { CustomerID = 2, Name = "David Gilmour" };
            yield return new Customer() { CustomerID = 3, Name = "James Page" };
        }
    }
}
