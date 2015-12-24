using System.ComponentModel.DataAnnotations;

namespace Orders.com.BLL.Domain
{
    public class Customer : DomainBase
    {
        public long CustomerID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public override long ID
        {
            set { CustomerID = value; }
            get { return CustomerID; }
        }
    }
}
