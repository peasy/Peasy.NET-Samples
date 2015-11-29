using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public abstract class OrdersDotComRepositoryBase<T> : RepositoryBase<T, long> where T : DomainBase, new()
    {
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
    }
}
