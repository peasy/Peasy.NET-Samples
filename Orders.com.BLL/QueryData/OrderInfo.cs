using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.com.BLL.QueryData
{
    public class OrderInfo
    {
        public long OrderID { get; set; }
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name ="Customer")]
        public string CustomerName { get; set; }
        public long CustomerID { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Total { get; set; }
        public string Status { get; set; }
        public bool HasShippedItems { get; set; }
    }
}
