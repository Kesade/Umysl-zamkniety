using System;

namespace Common.StorageEntities
{
    public interface IEntryEntity : IStorageEntity
    {
        int DiaryId { get; set; }
        int AuthorId { get; set; }
        string Body { get; set; }
        DateTime Timestamp { get; set; }
        string Title { get; set; }
        int Type { get; set; }
    }
}