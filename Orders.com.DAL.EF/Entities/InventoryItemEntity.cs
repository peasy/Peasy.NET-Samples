using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF.Entities
{
    [Table("InventoryItem")]
    public class InventoryItemEntity : InventoryItem
    {
        public virtual ProductEntity Product { get; set; }
    }
}
