using System;

namespace Common.DomainEntities
{
    public interface ICommentDomainEntity : IFormattable, IDomainEntity
        //Icomparable duty
    {
        DateTime Timestamp { get; set; }
        string Body { get; set; }
        IUserDomainEntity Author { get; set; }
        IEntryDomainEntity Entry { get; set; }
    }
}