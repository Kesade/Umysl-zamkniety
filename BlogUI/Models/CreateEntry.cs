using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class CreateEntry : ICreateEntry
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public int ParrentId { get; set; }
        public IDiaryDomainEntity Diary { get; set; }
        public IUserDomainEntity Author { get; set; }
    }
}