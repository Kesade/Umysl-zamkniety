using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Repositories;
using Common.StorageEntities;
using DomainModel.Extensions;
using Entities;
using Entities.Extensions;

namespace RepositoryHandlers.EntityFramework.Concrete
{
    public class DiaryRepositoryHandler : RepositoryHandler<IDiaryDomainEntity, Diaries>
    {
        public DiaryRepositoryHandler(IRepository<Diaries> blogRepository, IRepository<Entries> postRepository,
            IRepository<Users> userRepository) : base(blogRepository)
        {
            BlogRepository = blogRepository;
            PostRepository = postRepository;
            UserRepository = userRepository;
        }

        private IRepository<Diaries> BlogRepository { get; }
        private IRepository<Entries> PostRepository { get; }
        private IRepository<Users> UserRepository { get; }

        public override IDiaryDomainEntity ConvertToDomain(IStorageEntity obj)
        {
            return ((IDiaryEntity) obj).ConvertToDomain();
        }

        protected override async Task<IDiaryDomainEntity> UpdateAuthor(IDiaryDomainEntity toUpdate)
        {
            var user = await UserRepository.FindByIdAsync(toUpdate.Author.Id);

            toUpdate.Author = user.ConvertToDomain();
            return toUpdate;
        }


        public override IStorageEntity ConvertToEntity(IDiaryDomainEntity entities)
        {
            return entities.ConvertToEntity();
        }

        public new async Task<IDiaryDomainEntity> GetDomainMetaData(IDiaryDomainEntity entity)
        {
            var user = await UserRepository.FindByIdAsync(entity.Author.Id);

            entity.Author = user.ConvertToDomain();
            entity.Entries = (await PostRepository.GetQuerableSet().Where(x => x.DiaryId == entity.Id).ToListAsync())
                .Select(x => x.ConvertToDomain()).ToList();

            return entity;
        }
    }
}