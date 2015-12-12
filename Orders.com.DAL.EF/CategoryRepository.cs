using Orders.com.DAL.EF.Entities;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class CategoryRepository : OrdersDotComRepositoryBase<Category, CategoryEntity>, ICategoryDataProxy
    {
    }
}
