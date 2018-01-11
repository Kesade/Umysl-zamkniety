using Common.DomainEntities;

namespace Common.UI
{
    public interface IDisplayEntries
    {
        IDiaryDomainEntity Diary { get; set; }
        int Page { get; set; }
    }
}