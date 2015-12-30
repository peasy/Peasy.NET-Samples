using Orders.com.BLL.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orders.com.BLL.Extensions;

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

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Total
        {
            get { return OrderItems?.Sum(i => i.Amount); }
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

        public bool? HasUnsubmittedItems
        {
            get { return OrderItems?.Any(i => i.Entity.OrderStatus().CanSubmit); }
        }

        public bool? HasShippedItems
        {
            get { return OrderItems?.Any(i => i.Entity.OrderStatus().IsShipped); }
        }

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}