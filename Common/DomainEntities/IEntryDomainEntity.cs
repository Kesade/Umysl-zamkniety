using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.DomainEntities
{
    public interface IEntryDomainEntity : IDomainEntity
    {
        DateTime Timestamp { get; set; }
        string Body { get; set; }
        string Title { get; set; }
        EntryType Type { get; set; }
        IUserDomainEntity Author { get; set; }
        IDiaryDomainEntity Diary { get; set; }
        ICollection<ICommentDomainEntity> Comments { get; set; }
    }
}