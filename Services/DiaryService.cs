using Common.DomainEntities;
using Common.RepositoryHandlers;

namespace Services
{
    public class DiaryService : GenericService<IDiaryDomainEntity>
    {
        public DiaryService(IRepositoryHandler<IDiaryDomainEntity> handler) : base(handler)
        {
        }
    }
}