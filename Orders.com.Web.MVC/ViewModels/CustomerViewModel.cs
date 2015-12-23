using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC.ViewModels
{
    public class CustomerViewModel : ViewModel<Customer>
    {
        public long ID
        {
            get { return Entity.ID; }
            set { Entity.ID = value; }
        }

        public string Name
        {
            get { return Entity.Name; }
            set { Entity.Name = value; }
        }
    }
}