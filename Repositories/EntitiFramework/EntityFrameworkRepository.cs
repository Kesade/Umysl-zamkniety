using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Common.StorageEntities;

namespace Repositories.EntitiFramework
{
    public abstract class EntityFrameworkRepository<T> : IEntityFrameworkRepository<T> where T : class, IStorageEntity
    {
        protected EntityFrameworkRepository(DbContext context)
        {
            Context = context;
        }


        public async Task<T> CreateAsync(T obj)
        {
            var entity = GetSet().Add(obj);
            Context.Entry(obj).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<ICollection<T>> GetEntitiesAsync()
        {
            return await GetSet().ToListAsync();
        }

        IQueryable<T> IRepository<T>.GetQuerableSet()
        {
            return GetSet();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public DbSet<T> GetSet()
        {
            return Context.Set<T>();
        }

        public DbContext Context { get; }


        public virtual async Task RemoveAsync(T obj)
        {
            GetSet().Remove(obj);
            await Context.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, T obj)
        {
            if (obj == null) return;

            var db = await FindByIdAsync(id);
            await SetRefPropestiesToUpdate(obj, db);

            Context.Entry(db).CurrentValues.SetValues(obj);
            ExcludePropForUpdate(db);

            await Context.SaveChangesAsync();
        }

        protected abstract Task SetRefPropestiesToUpdate(T obj, T db);
        protected abstract void ExcludePropForUpdate(T db);
    }
}