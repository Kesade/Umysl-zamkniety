using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;

namespace Common.UI
{
    public interface IPageable<T> where T:IDomainEntity
    {
        Task<IPaged<T>> GetPagedSet(int diaryId, int page);
        int PageSize { get; }
    }
}