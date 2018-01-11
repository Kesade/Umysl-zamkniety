using System;
using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.RepositoryHandlers;
using Common.Services;
using Common.StorageEntities;

namespace Services
{
    public abstract class GenericService<T> :IService<T>, IDisposable where T : IDomainEntity
    {
        protected readonly IRepositoryHandler<T> Handler;

        protected GenericService(IRepositoryHandler<T> handler)
        {
            Handler = handler;
        }

        public void Dispose()
        {
            Handler.Dispose();
        }

        public async Task Delete(int id)
        {
            await Handler.RemoveAsync(id);
        }

        public async Task<T> GetById(int id)
        {
            return await Enrich(await Handler.GetDomainById(id));
        }

        private async Task<T> Enrich(T obj)
        {
            return await Handler.GetDomainMetaData(obj);
        }

        public async Task<IStorageEntity> GetEntityById(int id)
        {
            return await Handler.GetEntityById(id);
        }

        public async Task<IQueryable<T>> GetByParrent(int parrentId)
        {
            return (await Handler.GetDomainRepository()).Where(x => x.ParrentId == parrentId);
        }

        public async Task Put(T obj)
        {
            await Handler.CreateAsync(obj);
        }

        public virtual async Task<bool> Exists(T obj)
        {
            return await GetEntityById(obj.Id) != null;
        }

        public async Task<IQueryable<T>> GetRepository()
        {
            return await Handler.GetDomainRepository();
        }
    }
}