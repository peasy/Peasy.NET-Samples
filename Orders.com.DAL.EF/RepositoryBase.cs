using AutoMapper;
using Peasy.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public abstract class RepositoryBase<T, TKey> : IDataProxy<T, TKey> where T : class, IDomainObject<TKey>, new() 
    {
        protected abstract DbContext GetDbContext();

        public virtual IEnumerable<T> GetAll()
        {
            using (var context = GetDbContext())
            {
                OnBeforeGetAllExecuted(context);
                var data = context.Set<T>().Select(Mapper.Map<T, T>).ToArray();
                OnAfterGetAllExecuted(context, data);
                return data;
            }
        }

        protected void OnBeforeGetAllExecuted(DbContext context) { }
        protected void OnAfterGetAllExecuted(DbContext context, IEnumerable<T> result) { }

        public virtual T GetByID(TKey id)
        {
            using (var context = GetDbContext())
            {
                OnBeforeGetByIDExecuted(context);
                var data = context.Set<T>().Find(id);
                var mapped = Mapper.Map<T>(data);
                OnAfterGetByIDExecuted(context, mapped);
                return mapped;
            }
        }

        protected void OnBeforeGetByIDExecuted(DbContext context) { }
        protected void OnAfterGetByIDExecuted(DbContext context, T result) { }

        public virtual T Insert(T entity)
        {
            using (var context = GetDbContext())
            {
                OnBeforeInsertExecuted(context);
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Add(data);
                context.SaveChanges();
                entity.ID = data.ID;
                OnAfterInsertExecuted(context, entity);
                return entity;
            }
        }

        protected void OnBeforeInsertExecuted(DbContext context) { }
        protected void OnAfterInsertExecuted(DbContext context, T result) { }

        public virtual T Update(T entity)
        {
            using (var context = GetDbContext())
            {
                OnBeforeUpdateExecuted(context);
                var data = Mapper.Map(entity, default(T));
                context.Entry<T>(data).State = EntityState.Modified;
                context.SaveChanges();
                entity = Mapper.Map(data, entity);
                OnAfterUpdateExecuted(context, entity);
                return entity;
            }
        }

        protected void OnBeforeUpdateExecuted(DbContext context) { }
        protected void OnAfterUpdateExecuted(DbContext context, T result) { }

        public virtual void Delete(TKey id)
        {
            using (var context = GetDbContext())
            {
                OnBeforeDeleteExecuted(context);
                var entity = new T();
                entity.ID = id;
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.SaveChanges();
                OnAfterDeleteExecuted(context);
            }
        }

        protected void OnBeforeDeleteExecuted(DbContext context) { }
        protected void OnAfterDeleteExecuted(DbContext context) { }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = GetDbContext())
            {
                await OnBeforeGetAllAsyncExecuted(context);
                var data = await context.Set<T>().ToListAsync();
                var mapped = data.Select(Mapper.Map<T, T>).ToArray();
                await OnAfterGetAllAsyncExecuted(context, mapped);
                return mapped;
            }
        }

        protected async Task OnBeforeGetAllAsyncExecuted(DbContext context) { }
        protected async Task OnAfterGetAllAsyncExecuted(DbContext context, IEnumerable<T> result) { }

        public virtual async Task<T> GetByIDAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                await OnBeforeGetByIDExecutedAsync(context);
                var data = await context.Set<T>().FindAsync(id);
                var mapped = Mapper.Map<T>(data);
                await OnAfterGetByIDExecutedAsync(context, mapped);
                return mapped;
            }
        }

        protected async Task OnBeforeGetByIDExecutedAsync(DbContext context) { }
        protected async Task OnAfterGetByIDExecutedAsync(DbContext context, T result) { }

        public virtual async Task<T> InsertAsync(T entity)
        {
            using (var context = GetDbContext())
            {
                await OnBeforeInsertExecutedAsync(context);
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Add(data);
                await context.SaveChangesAsync();
                entity.ID = data.ID;
                await OnAfterInsertExecutedAsync(context, entity);
                return entity;
            }
        }

        protected async Task OnBeforeInsertExecutedAsync(DbContext context) { }
        protected async Task OnAfterInsertExecutedAsync(DbContext context, T result) { }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            using (var context = GetDbContext())
            {
                await OnBeforeUpdateExecutedAsync(context);
                var data = Mapper.Map(entity, default(T));
                context.Entry<T>(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
                entity = Mapper.Map(data, entity);
                await OnAfterUpdateExecutedAsync(context, entity);
                return entity;
            }
        }

        protected async Task OnBeforeUpdateExecutedAsync(DbContext context) { }
        protected async Task OnAfterUpdateExecutedAsync(DbContext context, T result) { }

        public virtual async Task DeleteAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                await OnBeforeDeleteExecutedAsync(context);
                var entity = new T();
                entity.ID = id;
                context.Entry<T>(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                await OnAfterDeleteExecutedAsync(context);
            }
        }

        protected async Task OnBeforeDeleteExecutedAsync(DbContext context) { }
        protected async Task OnAfterDeleteExecutedAsync(DbContext context) { }
    }
}
