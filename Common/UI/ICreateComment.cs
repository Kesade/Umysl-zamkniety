using Common.DomainEntities;

namespace Common.UI
{
    public interface ICreateComment
    {
        IUserDomainEntity Author { get; set; }
        string Body { get; set; }
        IEntryDomainEntity Entry { get; set; }
        int ParrentId { get; set; }
    }
}