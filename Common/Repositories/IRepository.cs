using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.StorageEntities;

namespace Common.Repositories
{
    public interface IRepository<TE>
        where TE : IStorageEntity
    {
        Task<TE> CreateAsync(TE obj);
        Task UpdateAsync(int id, TE obj);
        Task RemoveAsync(TE obj);
        Task<TE> FindByIdAsync(int id);
        Task<ICollection<TE>> GetEntitiesAsync();
        IQueryable<TE> GetQuerableSet();
        void Dispose();
    }
}