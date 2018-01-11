using System;

namespace Common.StorageEntities
{
    public interface IDiaryEntity : IStorageEntity
    {
        int AuthorId { get; set; }
        string Title { get; set; }
        DateTime Timestamp { get; set; }
    }
}