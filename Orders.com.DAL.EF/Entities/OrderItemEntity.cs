using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF.Entities
{
    [Table("OrderItem")]
    public class OrderItemEntity : OrderItem
    {
        public virtual OrderEntity Order { get; set; }
    }
}
