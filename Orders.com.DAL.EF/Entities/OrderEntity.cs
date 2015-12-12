using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF.Entities
{
    [Table("Order")]
    public class OrderEntity : Order
    {
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
