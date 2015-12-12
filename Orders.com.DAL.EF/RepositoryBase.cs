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
    public abstract class RepositoryBase<DTO, TEnt, TKey> : IDataProxy<DTO, TKey> where DTO : class, IDomainObject<TKey>, new()
                                                                                  where TEnt : class, IDomainObject<TKey>, new() 
    {
        protected abstract DbContext GetDbContext();

        public virtual IEnumerable<DTO> GetAll()
        {
            using (var context = GetDbContext())
            {
                var data = context.Set<TEnt>().AsNoTracking().Select(Mapper.Map<TEnt, DTO>).ToArray();
                return data;
            }
        }

        public virtual DTO GetByID(TKey id)
        {
            using (var context = GetDbContext())
            {
                var data = context.Set<TEnt>().Find(id);
                return Mapper.Map<TEnt, DTO>(data);
            }
        }

        public virtual DTO Insert(DTO entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(TEnt));
                context.Set<TEnt>().Add(data);
                context.SaveChanges();
                entity.ID = data.ID;
                return entity;
            }
        }

        public virtual DTO Update(DTO entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(TEnt));
                context.Set<TEnt>().Attach(data);
                context.Entry<TEnt>(data).State = EntityState.Modified;
                context.SaveChanges();
                entity = Mapper.Map(data, entity);
                return entity;
            }
        }

        public virtual void Delete(TKey id)
        {
            using (var context = GetDbContext())
            {
                var entity = new TEnt();
                entity.ID = id;
                context.Set<TEnt>().Attach(entity);
                context.Set<TEnt>().Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual async Task<IEnumerable<DTO>> GetAllAsync()
        {
            using (var context = GetDbContext())
            {
                var data = await context.Set<TEnt>().AsNoTracking().ToListAsync();
                return data.Select(Mapper.Map<TEnt, DTO>).ToArray();
            }
        }

        public virtual async Task<DTO> GetByIDAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                var data = await context.Set<TEnt>().FindAsync(id);
                return Mapper.Map<TEnt, DTO>(data);
            }
        }

        public virtual async Task<DTO> InsertAsync(DTO entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(TEnt));
                context.Set<TEnt>().Add(data);
                await context.SaveChangesAsync();
                entity.ID = data.ID;
                return entity;
            }
        }

        public virtual async Task<DTO> UpdateAsync(DTO entity)
        {
            using (var context = GetDbContext())
            {
                var data = Mapper.Map(entity, default(TEnt));
                context.Set<TEnt>().Attach(data);
                context.Entry<TEnt>(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
                entity = Mapper.Map(data, entity);
                return entity;
            }
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            using (var context = GetDbContext())
            {
                var entity = new TEnt();
                entity.ID = id; 
                context.Set<TEnt>().Attach(entity);
                context.Set<TEnt>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
