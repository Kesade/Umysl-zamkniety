//#pragma warning disable CS1998

using Common.DomainEntities;
using Common.Enums;
using Common.StorageEntities;

namespace DomainModel.Extensions
{
    public static class DomainExtensions
    {
        public static ICommentDomainEntity ConvertToDomain(this ICommentEntity entity)
        {
            return new Comment
            {
                Entry = new Entry {Id = entity.EntryId},
                Author = new User {Id = entity.AuthorId},
                Id = entity.Id,
                Body = entity.Body,
                Timestamp = entity.Timestamp
            };
        }

        public static IUserDomainEntity ConvertToDomain(this IUserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Roles = entity.Roles
            };
        }

        public static IEntryDomainEntity ConvertToDomain(this IEntryEntity entity)
        {
            return new Entry
            {
                Author = new User {Id = entity.AuthorId},
                Diary = new Diary {Id = entity.DiaryId},
                Id = entity.Id,
                Body = entity.Body,
                Timestamp = entity.Timestamp,
                Title = entity.Title,
                Type = (EntryType) entity.Type
            };
        }

        public static IDiaryDomainEntity ConvertToDomain(this IDiaryEntity entity)
        {
            return new Diary
            {
                Title = entity.Title,
                Id = entity.Id,
                Timestamp = entity.Timestamp,
                Author = new User {Id = entity.AuthorId}
            };
        }
    }
}
//#pragma warning restore CS1998
//    }
//}