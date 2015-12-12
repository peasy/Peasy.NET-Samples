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
                var data = context.Set<T>().Select(Mapper.Map<T, T>).ToArray();
                return data;
            }
        }

        public virtual T GetByID(TKey id)
        {
            using (var context = GetDbContext())
            {
                var data = context.Set<T>().Find(id);
                return Mapper.Map<T>(data);
            }
        }

        public virtual T Insert(T entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Add(data);
                context.SaveChanges();
                entity.ID = data.ID;
                return entity;
            }
        }

        public virtual T Update(T entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Attach(data);
                context.Entry<T>(data).State = EntityState.Modified;
                context.SaveChanges();
                entity = Mapper.Map(data, entity);
                return entity;
            }
        }

        public virtual void Delete(TKey id)
        {
            using (var context = GetDbContext())
            {
                var entity = new T();
                entity.ID = id;
                context.Set<T>().Attach(entity);
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = GetDbContext())
            {
                var data = await context.Set<T>().ToListAsync();
                return data.Select(Mapper.Map<T, T>).ToArray();
            }
        }

        public virtual async Task<T> GetByIDAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                var data = await context.Set<T>().FindAsync(id);
                return Mapper.Map<T>(data);
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Add(data);
                await context.SaveChangesAsync();
                entity.ID = data.ID;
                return entity;
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(T));
                context.Set<T>().Attach(data);
                context.Entry<T>(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
                entity = Mapper.Map(data, entity);
                return entity;
            }
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                var entity = new T();
                entity.ID = id; 
                context.Set<T>().Attach(entity);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
