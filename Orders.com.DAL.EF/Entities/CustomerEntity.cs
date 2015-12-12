using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF.Entities
{
    [Table("Customer")]
    public class CustomerEntity : Customer
    {
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
