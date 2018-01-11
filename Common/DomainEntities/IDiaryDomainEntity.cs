using System;
using System.Collections.Generic;

namespace Common.DomainEntities
{
    public interface IDiaryDomainEntity : IDomainEntity
    {
        string Title { get; set; }
        DateTime Timestamp { get; set; }
        ICollection<IEntryDomainEntity> Entries { get; set; }
        IUserDomainEntity Author { get; set; }
    }
}