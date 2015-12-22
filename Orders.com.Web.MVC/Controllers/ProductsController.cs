using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Orders.com.DAL.EF;
using Orders.com.Domain;
using Orders.com.BLL;

namespace Orders.com.Web.MVC.Controllers
{
    public class ProductsController : ControllerBase<Product>
    {
        public ProductsController(IProductService service) : base(service)
        {

        }
    }
}
