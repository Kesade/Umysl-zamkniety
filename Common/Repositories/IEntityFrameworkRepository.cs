using System.Data.Entity;
using Common.StorageEntities;

namespace Common.Repositories
{
    public interface IEntityFrameworkRepository<T> : IRepository<T> where T : class, IStorageEntity
    {
        DbContext Context { get; }
        DbSet<T> GetSet();
    }
}