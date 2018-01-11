using System;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Enums;
using Common.RepositoryHandlers;
using Common.UI;
using DomainModel;

namespace Services
{
    public class EntryService : GenericService<IEntryDomainEntity>
    {
        public EntryService(IRepositoryHandler<IEntryDomainEntity> handler) : base(handler)
        {
        }

        public async Task Put(ICreateEntry model)
        {
            await Put(new Entry
            {
                Timestamp = DateTime.Now,
                Body = model.Body,
                Title = model.Title,
                Diary = model.Diary,
                Author = model.Author,
                Type = EntryType.Blog
            });
        }
    }
}