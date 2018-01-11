using Common.DomainEntities;

namespace Entities.Extensions
{
    public static class EntityExtensions
    {
        public static Comments ConvertToEntity(this ICommentDomainEntity entity)
        {
            return new Comments
            {
                Id = entity.Id,
                Timestamp = entity.Timestamp,
                AuthorId = entity.Author.Id,
                Body = entity.Body,
                EntryId = entity.Entry.Id
            };
        }

        public static Entries ConvertToEntity(this IEntryDomainEntity entity)
        {
            return new Entries
            {
                Timestamp = entity.Timestamp,
                Title = entity.Title,
                Id = entity.Id,
                Type = (int) entity.Type,
                Body = entity.Body,
                AuthorId = entity.Author.Id,
                DiaryId = entity.Diary.Id
            };
        }


        public static Users ConvertToEntity(this IUserDomainEntity entity)
        {
            return new Users
            {
                Id = entity.Id,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Roles = entity.Roles
            };
        }

        public static Diaries ConvertToEntity(this IDiaryDomainEntity entity)
        {
            return new Diaries
            {
                AuthorId = entity.Author.Id,
                Id = entity.Id,
                Timestamp = entity.Timestamp,
                Title = entity.Title
            };
        }
    }
}