using System;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.RepositoryHandlers;
using Common.UI;
using DomainModel;

namespace Services
{
    public class CommentService : GenericService<ICommentDomainEntity>
    {
        public CommentService(IRepositoryHandler<ICommentDomainEntity> handler) : base(handler)
        {
        }

        public async Task Put(ICreateComment model)
        {
            await Put(new Comment
            {
                Author = model.Author,
                Timestamp = DateTime.Now,
                Body = model.Body,
                Entry = model.Entry
            });
        }
    }
}