using System.Collections.Generic;
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
    public class EntryRepositoryHandler : RepositoryHandler<IEntryDomainEntity, Entries>
    {
        public EntryRepositoryHandler(IRepository<Diaries> blogRepository,
            IRepository<Comments> commentRepository, IRepository<Entries> posRepository,
            IRepository<Users> userRepository) : base(posRepository)
        {
            BlogRepository = blogRepository;
            CommentRepository = commentRepository;
            PosRepository = posRepository;
            UserRepository = userRepository;
        }

        private IRepository<Diaries> BlogRepository { get; }
        private IRepository<Comments> CommentRepository { get; }
        private IRepository<Users> UserRepository { get; }
        private IRepository<Entries> PosRepository { get; }

        public override IEntryDomainEntity ConvertToDomain(IStorageEntity obj)
        {
            return ((IEntryEntity) obj).ConvertToDomain();
        }

        protected override async Task UpdateAuthor(IEntryDomainEntity entity)
        {
            var user = await UserRepository.FindByIdAsync(entity.Author.Id);
            entity.Author = user.ConvertToDomain();
        }

        public override IStorageEntity ConvertToEntity(IEntryDomainEntity entities)
        {
            return entities.ConvertToEntity();
        }

        public override async Task<IEntryDomainEntity> GetDomainMetaData(IEntryDomainEntity entity)
        {
            await UpdateAuthor(entity);
            entity.Diary = (await BlogRepository.FindByIdAsync(entity.Diary.Id)).ConvertToDomain();
            await UpdateComments(entity);
            
            return entity;
        }

        private async Task UpdateComments(IEntryDomainEntity entity)
        {
            var comments = (await CommentRepository.GetQuerableSet().Where(x => x.EntryId == entity.Id).ToListAsync())
                                .OrderByDescending(x => x.Timestamp).Select(x => x.ConvertToDomain()).ToList();
            
            foreach (var com in comments)
                com.Author = (await UserRepository.FindByIdAsync(com.Author.Id)).ConvertToDomain();

            entity.Comments = comments;
        }
    }
}