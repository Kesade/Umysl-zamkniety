using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Repositories;
using Common.StorageEntities;
using DomainModel.Extensions;
using Entities;
using Entities.Extensions;

namespace RepositoryHandlers.EntityFramework.Concrete
{
    public class CommentRepositoryHandler : RepositoryHandler<ICommentDomainEntity, Comments>
    {
        public CommentRepositoryHandler(IRepository<Comments> commentRepository, IRepository<Entries> postRepository,
            IRepository<Users> userRepository) : base(commentRepository)
        {
            CommentRepository = commentRepository;
            PostRepository = postRepository;
            UserRepository = userRepository;
        }

        private IRepository<Comments> CommentRepository { get; }
        private IRepository<Entries> PostRepository { get; }
        private IRepository<Users> UserRepository { get; }

        public override ICommentDomainEntity ConvertToDomain(IStorageEntity obj)
        {
            return ((ICommentEntity) obj).ConvertToDomain();
        }

        protected override async Task UpdateAuthor(ICommentDomainEntity toUpdate)
        {
            var user = await UserRepository.FindByIdAsync(toUpdate.Author.Id);
            toUpdate.Author = user.ConvertToDomain();
     
        }


        public override IStorageEntity ConvertToEntity(ICommentDomainEntity entities)
        {
            return entities.ConvertToEntity();
        }

        public override async Task<ICommentDomainEntity> GetDomainMetaData(ICommentDomainEntity entity)
        {
            await UpdateAuthor(entity);
            entity.Entry = (await PostRepository.FindByIdAsync(entity.Entry.Id)).ConvertToDomain();

            return entity;
        }
    }
}