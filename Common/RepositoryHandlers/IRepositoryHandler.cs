using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.StorageEntities;

namespace Common.RepositoryHandlers
{
    public interface IRepositoryHandler<TD> where TD : IDomainEntity
    {
        TD ConvertToDomain(IStorageEntity obj);
        IStorageEntity ConvertToEntity(TD entities);
        Task<TD> CreateAsync(IStorageEntity obj);
        Task<IQueryable<TD>> GetDomainRepository();
        Task<TD> GetDomainMetaData(TD entity);
        Task RemoveAsync(int id);
        Task<TD> GetDomainById(int id);
        Task<IStorageEntity> GetEntityById(int id);
        Task Put(int id, TD obj);
        void Dispose();
        Task<TD> CreateAsync(TD obj);
    }
}


//public async Task<ICollection<IDiaryDomainEntity>> GetDomainRepository()
//{
//return (ICollection<IDiaryDomainEntity>)await ConverToDomainsAsync(await Context.Set<Blogs>()
//.ToListAsync());
//}


//public async Task<IDiaryDomainEntity> FindByIdAsync(int id)
//{
//var x = await GetEntityById(id);
//return await x.ConvertToDomain();
//}


//public async Task<Blogs> GetEntityById(int id)
//{
//return await Context.Set<Blogs>().FindAsync(id);
//}


//public IQueryable<IStorageEntity> FindEntititesBy(DbContext context, Expression<Func<IStorageEntity, bool>> func)
//{
//return Context.Set<Blogs>().Where(func);
//}


//public async Task<IStorageEntity> ConvertToEntity(IDiaryDomainEntity entities)
//{
//return await entities.ConvertToEntity();
//}


//private async Task Delete(IDiaryDomainEntity obj)
//{
//var toDel = await Context.Set<Blogs>().FindAsync(obj.Id);
//if (toDel != null)
//Context.Set<Blogs>().Remove(toDel);
//}


//public async Task<IDiaryDomainEntity> UpdateState(IDiaryDomainEntity toUpdate)
//{
//return await GetDomainMetaData(toUpdate);
//}

//public override async Task<IDomainEntity> ConvertToDomain(Blogs entity)
//{
//return await entity.ConvertToDomain();
//}