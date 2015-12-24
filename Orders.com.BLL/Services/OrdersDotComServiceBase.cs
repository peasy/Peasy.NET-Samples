using Peasy;
using Peasy.Core;
using Orders.com.BLL.DataProxy;

namespace Orders.com.BLL.Services
{
    public abstract class OrdersDotComServiceBase<T> : BusinessServiceBase<T, long> where T : IDomainObject<long>, new()
    {
        public OrdersDotComServiceBase(IOrdersDotComDataProxy<T> dataProxy) : base(dataProxy)
        {
        }
    }
}
