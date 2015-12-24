using System;

namespace Orders.com.BLL.QueryData
{
    public class OrderInfo
    {
        public long OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public long CustomerID { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public bool HasShippedItems { get; set; }
    }
}
