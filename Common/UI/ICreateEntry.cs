using Common.DomainEntities;

namespace Common.UI
{
    public interface ICreateEntry
    {
        string Body { get; set; }
        string Title { get; set; }
        IDiaryDomainEntity Diary { get; set; }
        IUserDomainEntity Author { get; set; }
        int ParrentId { get; set; }
    }
}