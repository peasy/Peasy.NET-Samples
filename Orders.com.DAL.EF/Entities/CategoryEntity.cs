using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF.Entities
{
    [Table("Category")]
    public class CategoryEntity : Category
    {
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
