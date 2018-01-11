using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;

namespace Common.Services
{
    public interface IService<TD>
    {
        Task<IQueryable<TD>> GetRepository();
        Task<TD> GetById(int id);
        Task Put(TD obj);
        Task Delete(int id);
        void Dispose();
        Task<IQueryable<TD>> GetByParrent(int id);
    }
}