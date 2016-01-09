using Orders.com.BLL.Domain;
using Peasy;
using Peasy.DataProxy.EF6;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public abstract class OrdersDotComRepositoryBase<T> : EF6DataProxyBase<T, T, long>, IServiceDataProxy<T, long> where T : DomainBase, new()
    {
        public OrdersDotComRepositoryBase() : base(new MapsterHelper())
        {
        }

        protected override DbContext GetDbContext()
        {
            return new OrdersDotComContext();
        }

        public override T Insert(T entity)
        {
            entity.CreatedDatetime = DateTime.Now;
            return base.Insert(entity);
        }

        public override Task<T> InsertAsync(T entity)
        {
            entity.CreatedDatetime = DateTime.Now;
            return base.InsertAsync(entity);
        }

        public override T Update(T entity)
        {
            entity.LastModifiedDatetime = DateTime.Now;
            return base.Update(entity);
        }

        public override Task<T> UpdateAsync(T entity)
        {
            entity.LastModifiedDatetime = DateTime.Now;
            return base.UpdateAsync(entity);
        }

        public bool SupportsTransactions
        {
            get { return true; }
        }

        public bool IsLatencyProne
        {
            get { return false; }
        }
    }
}
