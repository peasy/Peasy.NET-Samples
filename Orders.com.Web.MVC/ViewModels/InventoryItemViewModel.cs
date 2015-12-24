using Orders.com.BLL.Domain;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.Web.MVC.ViewModels
{
    public class InventoryItemViewModel : ViewModel<InventoryItem>
    {
        public long ID
        {
            get { return Entity.ID; }
            set { Entity.ID = value; }
        }

        [Display(Name ="Name")]
        public string ProductName { get; set; }

        [Display(Name ="Quantity")]
        public decimal QuantityOnHand
        {
            get { return Entity.QuantityOnHand; }
            set { Entity.QuantityOnHand = value; }
        }

        public long ProductID
        {
            get { return Entity.ProductID; }
            set { Entity.ProductID = value; }
        }

        public string Version
        {
            get { return Entity.Version; }
            set { Entity.Version = value; }
        }
    }
}