using System;

namespace Common.StorageEntities
{
    public interface ICommentEntity : IStorageEntity
    {
        int AuthorId { get; set; }
        int EntryId { get; set; }
        string Body { get; set; }
        DateTime Timestamp { get; set; }
    }
}