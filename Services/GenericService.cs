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
            return await Handler.GetDomainMetaData(await Handler.GetDomainById(id));
        }

        public async Task<T> Enrich(T obj)
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

        public async Task<bool> Exists(int id)
        {
            return await Handler.GetDomainById(id) != null;
        }

        public async Task<IQueryable<T>> GetRepository()
        {
            return await Handler.GetDomainRepository();
        }
    }
}