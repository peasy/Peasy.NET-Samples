using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
	public class OrdersDotComContext : DbContext
    {
		public DbSet<Customer> Customers { get; set; }
    }
}
