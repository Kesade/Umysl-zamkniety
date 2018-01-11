using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class DisplayBlog : IDisplayEntries
    {
        public IDiaryDomainEntity Diary { get; set; }
        public int Page { get; set; }
    }
}