using System;
using System.Data.Entity;
using Orders.com.Domain;
using Peasy.Core;

namespace Orders.com.DAL.EF
{
    public class CustomersRepository : OrdersDotComRepositoryBase<Customer> 
    {
        protected override DbContext GetDbContext()
        {
            return new OrdersDotComContext();
        }
    }
}
