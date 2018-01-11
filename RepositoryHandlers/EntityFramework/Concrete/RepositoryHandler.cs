using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Repositories;
using Common.RepositoryHandlers;
using Common.StorageEntities;

namespace RepositoryHandlers.EntityFramework.Concrete
{
    public abstract class RepositoryHandler<TD, TE> : IRepositoryHandler<TD>
        where TD : IDomainEntity
        where TE : IStorageEntity
    {
        private readonly IRepository<TE> _repository;

        protected RepositoryHandler(IRepository<TE> repository)
        {
            _repository = repository;
        }

        public async Task<IStorageEntity> GetEntityById(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public abstract IStorageEntity ConvertToEntity(TD entities);

        public virtual async Task<TD> CreateAsync(IStorageEntity obj)
        {
            var ins = await _repository.CreateAsync((TE) obj);

            return await GetDomainMetaData(ConvertToDomain(ins));
        }

        public async Task<IQueryable<TD>> GetDomainRepository()
        {
            var set = (await _repository.GetEntitiesAsync()).Select(x => ConvertToDomain(x));

            var enumerable = set as TD[] ?? set.ToArray();

            foreach (var d in enumerable)
                await UpdateAuthor(d);
            return enumerable.AsQueryable();
        }

        public virtual async Task<TD> GetDomainMetaData(TD entity)
        {
            var obj = await GetEntityById(entity.Id);

            return ConvertToDomain(obj);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync((TE) await GetEntityById(id));
        }


        public Task Put(int id, TD obj)
        {
            return _repository.UpdateAsync(id, (TE) ConvertToEntity(obj));
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<TD> CreateAsync(TD obj)
        {
            var ins = await _repository.CreateAsync((TE) ConvertToEntity(obj));

            return await GetDomainMetaData(ConvertToDomain(ins));
        }

        public abstract TD ConvertToDomain(IStorageEntity obj);

        public async Task<TD> GetDomainById(int id)
        {
            return ConvertToDomain(await GetEntityById(id));
        }

        protected abstract Task UpdateAuthor(TD toUpdate);
    }
}