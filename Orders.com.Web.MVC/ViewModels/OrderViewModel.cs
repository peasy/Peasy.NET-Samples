using Orders.com.BLL.Domain;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Orders.com.Web.MVC.ViewModels
{
    public class OrderViewModel : ViewModel<Order>
    {
        public string CustomerName { get; set; }

        public long CustomerID
        {
            get { return Entity.CustomerID; }
            set { Entity.CustomerID = value; }
        }

        public DateTime OrderDate
        {
            get { return Entity.OrderDate; }
            set { Entity.OrderDate = value; }
        }

        public long ID
        {
            get { return Entity.ID; }
            set { Entity.ID = value; }
        }

        public Customer AssociatedCustomer
        {
            get
            {
                return Customers.FirstOrDefault(c => c.CustomerID == CustomerID);
            }
        }

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}