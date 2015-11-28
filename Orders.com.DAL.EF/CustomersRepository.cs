using System;
using System.Data.Entity;
using Orders.com.Domain;
using Peasy.Core;

namespace Orders.com.DAL.EF
{
    public class CustomersRepository : RepositoryBase<Customer, long>, IDataProxy<Customer, long>
    {
        protected override DbContext GetDbContext()
        {
            return new OrdersDotComContext();
        }
    }
}
