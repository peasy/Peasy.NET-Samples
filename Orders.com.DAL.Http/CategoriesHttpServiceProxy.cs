using Orders.com.BLL.DataProxy;
using Orders.com.BLL.Domain;

namespace Orders.com.DAL.Http
{
    public class CategoriesHttpServiceProxy : OrdersDotComHttpProxyBase<Category, long>, ICategoryDataProxy
    {
        public CategoriesHttpServiceProxy(string baseAddress) :base(baseAddress) { }

        protected override string RequestUri
        {
            get { return $"{BaseAddress}/categories"; }
        }
    }
}
